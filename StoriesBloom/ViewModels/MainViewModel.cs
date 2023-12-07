namespace StoriesBloom.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    readonly StoryDataService _dataService;


    [ObservableProperty]
    ObservableCollection<StoryDetail> items;

    [ObservableProperty]
    ObservableCollection<StoryInfo> storiesInfo;

    [ObservableProperty]
    ObservableCollection<StoryInfo> storiesInfo2;

    [ObservableProperty]
    ObservableCollection<Category> storiesCategories;

    public MainViewModel(StoryDataService dataService, ICategoriesService categoriesService)
    {
        _dataService= dataService;
        InitializeTales();
        StoriesCategories = new ObservableCollection<Category>(categoriesService.Categories);
    }

    private void InitializeTales()
    {
        StoriesInfo = new ObservableCollection<StoryInfo>
               {
                   new StoryInfo { Name = "Kopciuszek", ReadTime = new TimeSpan(0, 30, 0), Image = "cinderella.jpg" },
                   new StoryInfo { Name = "Królewna Śnieżka", ReadTime = new TimeSpan(0, 25, 0),  Image = "snow.jpg" },
                   new StoryInfo { Name = "Roszpunka", ReadTime = new TimeSpan(0, 20, 0),  Image = "rapunzel.jpg" },
                   new StoryInfo { Name = "Czerwony Kapturek", ReadTime = new TimeSpan(0, 15, 0),  Image = "hood.jpg" },
                   new StoryInfo { Name = "Piękna i Bestia", ReadTime = new TimeSpan(0, 35, 0),  Image = "beauty.jpg" }
               };
        StoriesInfo2 = new ObservableCollection<StoryInfo>
               {
                   new StoryInfo { Name = "Królewna Śnieżka", ReadTime = new TimeSpan(0, 25, 0),  Image = "snow.jpg" },
                   new StoryInfo { Name = "Roszpunka", ReadTime = new TimeSpan(0, 20, 0),  Image = "rapunzel.jpg" },
                   new StoryInfo { Name = "Czerwony Kapturek", ReadTime = new TimeSpan(0, 15, 0),  Image = "hood.jpg" },
                   new StoryInfo { Name = "Piękna i Bestia", ReadTime = new TimeSpan(0, 35, 0),  Image = "beauty.jpg" },
                   new StoryInfo { Name = "Kopciuszek", ReadTime = new TimeSpan(0, 30, 0),  Image = "cinderella.jpg" }
               };

    }

    [RelayCommand]
    public async Task LoadMore()
    {
        var items = await _dataService.GetStories();

        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    public async Task LoadDataAsync()
    {
        Items = new ObservableCollection<StoryDetail>(await _dataService.GetStories());
    }


    [RelayCommand]
    private async void GoToDetails(StoryDetail item)
    {
        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", item }
        });
    }
}
