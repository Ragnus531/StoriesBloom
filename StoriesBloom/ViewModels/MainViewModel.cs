namespace StoriesBloom.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    readonly StoryDataService _dataService;


    [ObservableProperty]
    ObservableCollection<StoryDetail> items;

    public MainViewModel(StoryDataService dataService)
    {
        _dataService= dataService;
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
