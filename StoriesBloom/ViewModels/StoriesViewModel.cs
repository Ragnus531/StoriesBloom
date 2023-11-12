using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

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

    public ICommand IncrementCounterCommand { get; }
    public ICommand GoToNovelDetailsCommand { get; }

    public StoriesViewModel(StoryDataService service)
	{
		dataService = service;
        IncrementCounterCommand = new RelayCommand(ChangeElements);
        GoToNovelDetailsCommand = new AsyncRelayCommand(GoToDetail);
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

	private void ChangeElements()
	{
		var mm = Category;
		//Change elements to specific category
		Console.WriteLine("dasds");
	}

	private async Task GoToDetail()
	{
        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", new StoryDetail("f","f","f","f","f","f","f","f","d") }
        });
    }
}
