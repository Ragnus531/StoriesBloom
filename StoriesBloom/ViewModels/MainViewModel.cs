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

    //Add a cache or a await while await we are loading those stories maybe and saving them in cache?
    public MainViewModel(StoryDataService dataService, ICategoriesService categoriesService,
        IPopupService popupService)
    {
        _dataService = dataService;
        _popupService = popupService;
        InitializeTales();
        StoriesCategories = new ObservableCollection<Category>(categoriesService.Categories);
    }

    private void InitializeTales()
    {
        List<StoryDetail> randomStories = (List<StoryDetail>)_dataService.Get5RandomStories();
        int minutes1 = CalculateTimeToRead(randomStories[0]); int minutes2 = CalculateTimeToRead(randomStories[1]);
        int minutes3 = CalculateTimeToRead(randomStories[2]); int minutes4 = CalculateTimeToRead(randomStories[3]);
        int minutes5 = CalculateTimeToRead(randomStories[4]);

        //Random 5 stories 
        StoriesInfo = new ObservableCollection<StoryInfo>
               {
                   new(){ Name = randomStories[0].Title, ReadTime = new TimeSpan(0, minutes1, 0),  Image = "/stories/cinderella.jpg" },
                   new(){ Name = randomStories[1].Title, ReadTime = new TimeSpan(0, minutes2, 0),  Image = "/stories/snow.jpg" },
                   new(){ Name = randomStories[2].Title, ReadTime = new TimeSpan(0, minutes3, 0),  Image = "/stories/rapunzel.jpg" },
                   new(){ Name = randomStories[3].Title, ReadTime = new TimeSpan(0, minutes4, 0),  Image = "/stories/hood.jpg" },
                   new(){ Name = randomStories[4].Title, ReadTime = new TimeSpan(0, minutes5, 0),  Image = "/stories/beauty.jpg" }
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

    public async Task LoadDataAsync()
    {
        Items = new ObservableCollection<StoryDetail>(_dataService.GetStories());
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
}
