using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using StoriesBloom.Views.Popups;
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Mvvm.Messaging;
using StoriesBloom.Messages;

namespace StoriesBloom.ViewModels;

[QueryProperty(nameof(Category), "Category")]
public partial class StoriesViewModel : BaseViewModel
{

	private readonly StoryDataService dataService;
    private readonly ICategoriesService _categoriesService;

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
	ObservableCollection<StoryDetail> items = new ObservableCollection<StoryDetail>();

    public ICommand IncrementCounterCommand { get; }
    public ICommand GoToNovelDetailsCommand { get; }

	private string _cachedCategory;
	private bool _viewInitialized;

    public StoriesViewModel(StoryDataService service, ICategoriesService categoriesService)
	{
		dataService = service;
        _categoriesService = categoriesService;
        IncrementCounterCommand = new AsyncRelayCommand(ChangeElements);
        GoToNovelDetailsCommand = new AsyncRelayCommand<StoryDetail>(GoToDetail);
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

	public void InitListener()
	{
        //There is problem with picker and ArgsConverter when we don't go to page and initialize components first
		//That is why we are using this hack here to postpone initializing items with category.
        WeakReferenceMessenger.Default.Register<ChangedCategoryMessage>(this, (r, m) =>
        {
			if (!_viewInitialized)
			{
				_cachedCategory = m.Value;
			}
			else
			{
				MainThread.BeginInvokeOnMainThread(() =>
				{
					Category = m.Value;
					ChangeElements();
				});
			}
        });	
    }

	public void InitCategory()
	{
		if(!string.IsNullOrEmpty(_cachedCategory))
		{
			Category = _cachedCategory;
			ChangeElements();
            _cachedCategory = null; //To not trigger again with wrong category
		}
	}

	public void InitApp()
	{
		_viewInitialized = true;
		if (string.IsNullOrEmpty(Category))
		{
            Category = _categoriesService.Categories.FirstOrDefault()?.Name;
		}

		if(Items.Count == 0)
		{
			ChangeElements();
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

	private async Task ChangeElements()
	{
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
