using CommunityToolkit.Mvvm.Messaging;
using StoriesBloom.Messages;
using System.Windows.Input;

namespace StoriesBloom.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    readonly StoryDataService _dataService;
    private readonly IPopupService _popupService;
    [ObservableProperty]
    ObservableCollection<StoryDetail> items;

    [ObservableProperty]
    ObservableCollection<StoryInfo> storiesInfo;

    [ObservableProperty]
    ObservableCollection<Category> storiesCategories;

    [ObservableProperty]
    bool storyDetailLoading;


    public ICommand CategoryChoosenCommand { get; }
    public ICommand StoryChoosenCommand { get; }

    private List<StoryDetail> _storyDetails = new List<StoryDetail>();

    //Add a cache or a await while await we are loading those stories maybe and saving them in cache?
    public MainViewModel(StoryDataService dataService, ICategoriesService categoriesService,
        IPopupService popupService)
    {
        _dataService = dataService;
        _popupService = popupService;
        InitializeTales();
        StoriesCategories = new ObservableCollection<Category>(categoriesService.Categories);
        StoryChoosenCommand = new AsyncRelayCommand<StoryInfo>(GoToStory);
        CategoryChoosenCommand = new RelayCommand<Category>(GoToCategory);
    }

    private void InitializeTales()
    {
        List<StoryDetail> randomStories = (List<StoryDetail>)_dataService.Get5RandomStories();
        int minutes1 = CalculateTimeToRead(randomStories[0]); int minutes2 = CalculateTimeToRead(randomStories[1]);
        int minutes3 = CalculateTimeToRead(randomStories[2]); int minutes4 = CalculateTimeToRead(randomStories[3]);
        int minutes5 = CalculateTimeToRead(randomStories[4]);

        _storyDetails.AddRange(randomStories);

        //Random 5 stories 
        StoriesInfo = new ObservableCollection<StoryInfo>
               {
                   new(){ Name = randomStories[0].Title, ReadTime = new TimeSpan(0, minutes1, 0),  Image = randomStories[0].ImagePath },
                   new(){ Name = randomStories[1].Title, ReadTime = new TimeSpan(0, minutes2, 0),  Image = randomStories[1].ImagePath },
                   new(){ Name = randomStories[2].Title, ReadTime = new TimeSpan(0, minutes3, 0),  Image = randomStories[2].ImagePath },
                   new(){ Name = randomStories[3].Title, ReadTime = new TimeSpan(0, minutes4, 0),  Image = randomStories[3].ImagePath },
                   new(){ Name = randomStories[4].Title, ReadTime = new TimeSpan(0, minutes5, 0),  Image = randomStories[4].ImagePath }
               };

        //StoriesInfo = new ObservableCollection<StoryInfo>
        //       {
        //           new StoryInfo { Name = "Kopciuszek", ReadTime = new TimeSpan(0, 30, 0), Image = "/stories/cinderella.jpg" },
        //           new StoryInfo { Name = "Królewna Śnieżka", ReadTime = new TimeSpan(0, 25, 0),  Image = "/stories/snow.jpg" },
        //           new StoryInfo { Name = "Roszpunka", ReadTime = new TimeSpan(0, 20, 0),  Image = "/stories/rapunzel.jpg" },
        //           new StoryInfo { Name = "Czerwony Kapturek", ReadTime = new TimeSpan(0, 15, 0),  Image = "/stories/hood.jpg" },
        //           new StoryInfo { Name = "Piękna i Bestia", ReadTime = new TimeSpan(0, 35, 0),  Image = "/stories/beauty.jpg" }
        //       };
    }

    [RelayCommand]
    public async Task LoadMore()
    {
        var items = _dataService.GetStories();

        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    [RelayCommand]
    private async void GoToDetails(StoryDetail item)
    {
        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", item }
        });
    }

    private static int CalculateTimeToRead(StoryDetail storyDetail)
    {
        int number = storyDetail.Title.Length +
                     storyDetail.Chapter1.Length +
                     storyDetail.Chapter2.Length +
                     storyDetail.Chapter3.Length +
                     storyDetail.Chapter4.Length +
                     storyDetail.Chapter5.Length +
                     storyDetail.Epilogue.Length;

        if (storyDetail.UnexpectedTwist != null)
        {
            number += storyDetail.UnexpectedTwist.Length;
        }
        return ReadTimeFromNumberOfWordsAsMinutes(number);
    }

    private static int ReadTimeFromNumberOfWordsAsMinutes(int numberOfWords)
    {
        int averageReadingSpeed = 250;
        int minutes = numberOfWords / averageReadingSpeed;
        return minutes;
    }

    private async Task GoToStory(StoryInfo storyInfo)
    {
        StoryDetailLoading = true;

        var storyDet = _storyDetails.FirstOrDefault(a => a.Title == storyInfo.Name);
        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", storyDet }
        });

        StoryDetailLoading = false;
    }

    private  void GoToCategory(Category category)
    {
        ((AppShell)App.Current.MainPage).SwitchtoTab(1);
        WeakReferenceMessenger.Default.Send(new ChangedCategoryMessage(category.Name));

        //await Shell.Current.GoToAsync(nameof(StoriesPage), true, new Dictionary<string, object>
        //{
        //    { "Category", category }
        //});
    }

    public void ResetState()
    {
        StoryDetailLoading = false;

    }
}
