using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using StoriesBloom.Views.Popups;
using CommunityToolkit.Maui.Converters;

namespace StoriesBloom.ViewModels;


public partial class StoriesViewModel : BaseViewModel
{
	readonly StoryDataService dataService;

	[ObservableProperty]
	bool isLoadingData = true;

	[ObservableProperty]
	string currState = States.Loading;

    [ObservableProperty]
    string category;

	[ObservableProperty]
	bool canStateChange;

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
		var items = dataService.GetStories();

		foreach (var item in items)
		{
			Items.Add(item);
		}
	}

	public async Task LoadDataAsync()
	{
		CurrState = States.Loading;
		var dd = dataService.GetStories();
        await Task.Run(() =>
        {
            Items = new ObservableCollection<StoryDetail>(dataService.GetStories());
            CurrState = States.Success;
        });
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
        //_popupService.ShowPopup(new LoadingStoriesPopupPage());
        //await Task.Delay(5000);
        //Items = new ObservableCollection<StoryDetail>(dataService.GetStories(Category));
        CurrState = States.Loading;
        var ff = dataService.GetStories(Category);
        Items.Clear();
        await Task.Run(() =>
		{
			foreach (var item in ff)
			{
				Items.Add(item);
			}
            CurrState = States.Success;
        });	
        //Items = new ObservableCollection<StoryDetail>(await dataService.GetStories(Category));
        //_popupService.HidePopup();
    }

    private async Task GoToDetail(StoryDetail novelDetail)
	{
        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", novelDetail }
        });
    }
    static class States
    {
        public const string Loading = nameof(Loading);
        public const string Success = nameof(Success);
    }
}
