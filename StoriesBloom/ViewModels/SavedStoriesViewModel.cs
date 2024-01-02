using StoriesBloom.Helpers;
using System.Windows.Input;

namespace StoriesBloom.ViewModels;

public partial class SavedStoriesViewModel : BaseViewModel
{
    [ObservableProperty]
    string currState = States.Loading;

    [ObservableProperty]
    ObservableCollection<StoryDetail> items = new ObservableCollection<StoryDetail>();

    [ObservableProperty]
	private bool storyDetailLoading;

    private ISavedStoryService _savedStoryService;

    public ICommand GoToNovelDetailsCommand { get; }

    public string LocalizedText => StoriesBloom.Resources.Strings.AppResources.HelloMessage;
    public Task Initialization { get; private set; }

    public SavedStoriesViewModel(ISavedStoryService savedStoryService)
    {
        GoToNovelDetailsCommand = new AsyncRelayCommand<StoryDetail>(GoToDetail);
        _savedStoryService = savedStoryService;

        // Initialization = InitializeAsync();

        Task.Run(async () =>
        {
            await InitData();
        });

    }

    private async Task InitializeAsync()
    {
        // Asynchronously initialize this instance.
        CurrState = States.Loading;
        var savedStories = _savedStoryService.GetAll();
        Items.Clear();
        await Task.Run(() =>
        {
            foreach (var item in savedStories)
            {
                Console.WriteLine("Adding");
                Items.Add(item);
            }
            CurrState = States.Success;
        });
    }

    public async Task InitData()
    {
        CurrState = States.Loading;
        var savedStories = _savedStoryService.GetAll();
        Items.Clear();
        await Task.Run(() =>
        {
            foreach (var item in savedStories)
            {
                Console.WriteLine("Adding");
                Items.Add(item);
            }
            CurrState = States.Success;
        });
    }

    private async Task GoToDetail(StoryDetail novelDetail)
    {
        StoryDetailLoading = true;
        await Task.Delay(100);

        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", novelDetail }
        });

        StoryDetailLoading = false;
    }
}
