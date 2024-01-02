using CommunityToolkit.Mvvm.Messaging;
using StoriesBloom.Helpers;
using StoriesBloom.Messages;
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
    private bool _viewInitialized;

    public SavedStoriesViewModel(ISavedStoryService savedStoryService)
    {
        GoToNovelDetailsCommand = new AsyncRelayCommand<StoryDetail>(GoToDetail);
        _savedStoryService = savedStoryService;

        // Initialization = InitializeAsync();
        _viewInitialized = true;

        //Task.Run(async () =>
        //{
        //    await InitData();
        //});

    }

    public async Task InitData()
    {
        CurrState = States.Loading;
        var savedStories = _savedStoryService.GetAll();
        if (!savedStories.Any())
        {
            CurrState = States.Empty;
            return;
        }
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
        //if(novelDetail.Saved == false)
        //{
        //    for (int i = 0; i < Items.Count; i++)
        //    {
        //        Items[i].Saved = true;
        //    }
        //    novelDetail.Saved = true;
        //}

        novelDetail.Saved = true;
        StoryDetailLoading = true;
        await Task.Delay(100);

        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", novelDetail }
        });

        StoryDetailLoading = false;
    }

    public void InitListener()
    {
        WeakReferenceMessenger.Default.Register<AddedSavedStoryMessage>(this, (r, m) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (_viewInitialized)
                {
                    m.Value.Saved = true;
                    Items.Add(m.Value);
                    if(Items.Count > 0)
                    {
                        CurrState = States.Success;
                    }
                }                
            });
        });

        WeakReferenceMessenger.Default.Register<DeletedSavedStoryMessage>(this, (r, m) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (_viewInitialized)
                {
                    for(int i = 0;i<Items.Count; i++)
                    {
                        if (Items[i].Title == m.Value.Title)
                        {
                            Items.RemoveAt(i);
                            break;
                        }
                    }

                    if (Items.Count == 0)
                    {
                        CurrState = States.Empty;
                    }
                }
            });
        });
    }

}
