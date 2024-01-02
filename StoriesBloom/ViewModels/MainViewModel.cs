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

    [ObservableProperty]
    bool categoryLoading;

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
        CategoryChoosenCommand = new AsyncRelayCommand<Category>(GoToCategory);
        InitListeners();
    }

    private void InitListeners()
    {
        WeakReferenceMessenger.Default.Register<AddedSavedStoryMessage>(this, (r, m) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var storyToUpdate = _storyDetails.FirstOrDefault(s => s.Title == m.Value.Title);
                if (storyToUpdate != null)
                {
                    storyToUpdate.Saved = true;
                }
            });
        });

        WeakReferenceMessenger.Default.Register<DeletedSavedStoryMessage>(this, (r, m) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var storyToUpdate = _storyDetails.FirstOrDefault(s => s.Title == m.Value.Title);
                if (storyToUpdate != null)
                {
                    storyToUpdate.Saved = false;
                }
            });
        });
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
                   new(){ Name = randomStories[0].Title, ReadTime = new TimeSpan(0, minutes1, 0),
                                Image = randomStories[0].ImagePath },
                   new(){ Name = randomStories[1].Title, ReadTime = new TimeSpan(0, minutes2, 0),
                                Image = randomStories[1].ImagePath },
                   new(){ Name = randomStories[2].Title, ReadTime = new TimeSpan(0, minutes3, 0),
                                Image = randomStories[2].ImagePath },
                   new(){ Name = randomStories[3].Title, ReadTime = new TimeSpan(0, minutes4, 0),
                                Image = randomStories[3].ImagePath },
                   new(){ Name = randomStories[4].Title, ReadTime = new TimeSpan(0, minutes5, 0),
                                Image = randomStories[4].ImagePath }
               };
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
        SwitchLoadingState(true);
        var storyDet = _storyDetails.FirstOrDefault(a => a.Title == storyInfo.Name);
        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", storyDet }
        });

        SwitchLoadingState(false);
    }

    private async Task GoToCategory(Category category)
    {
        SwitchLoadingState(true);
        await Task.Delay(500);

        ((AppShell)App.Current.MainPage).SwitchtoTab(1);
        WeakReferenceMessenger.Default.Send(new ChangedCategoryMessage(category.Name));
        SwitchLoadingState(false);
    }

    private void SwitchLoadingState(bool  state)
    {
        if (state)
        {
            CategoryLoading = true;
            StoryDetailLoading = true;
        }
        else
        {
            CategoryLoading = false;
            StoryDetailLoading = false;
        }
    }

    public void ResetState()
    {
        StoryDetailLoading = false;
        CategoryLoading = false;
    }
}
