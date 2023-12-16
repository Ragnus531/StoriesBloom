using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using StoriesBloom.Views.Popups;

namespace StoriesBloom.ViewModels;

public partial class StoriesViewModel : BaseViewModel
{
	readonly StoryDataService dataService;

    [ObservableProperty]
    string category;

    [ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	ObservableCollection<StoryDetail> items;
    private readonly IPopupService _popupService;

    public ICommand IncrementCounterCommand { get; }
    public ICommand GoToNovelDetailsCommand { get; }

    public StoriesViewModel(StoryDataService service, IPopupService popupService)
	{
		dataService = service;
        IncrementCounterCommand = new AsyncRelayCommand(ChangeElements);
        GoToNovelDetailsCommand = new AsyncRelayCommand<StoryDetail>(GoToDetail);
		_popupService = popupService;
    }

    [RelayCommand]
	private async void OnRefreshing()
	{
		IsRefreshing = true;

		try
		{
			await LoadDataAsync();
		}
		finally
		{
			IsRefreshing = false;
		}
	}

	[RelayCommand]
	public async Task LoadMore()
	{
		var items = await dataService.GetStories();

		foreach (var item in items)
		{
			Items.Add(item);
		}
	}

	public async Task LoadDataAsync()
	{
		Items = new ObservableCollection<StoryDetail>(await dataService.GetStories());
	}

	[RelayCommand]
	private async void GoToDetails(StoryDetail item)
	{
		await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
		{
			{ "Item", item }
		});
	}

	private async Task ChangeElements()
	{
		_popupService.ShowPopup(new LoadingStoriesPopupPage());

		Items = new ObservableCollection<StoryDetail>(await dataService.GetStories(Category));

		_popupService.HidePopup();
    }

	private async Task GoToDetail(StoryDetail novelDetail)
	{
        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", novelDetail }
        });
    }
}
