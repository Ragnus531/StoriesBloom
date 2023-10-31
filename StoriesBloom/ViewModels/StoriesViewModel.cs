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

	public StoriesViewModel(StoryDataService service)
	{
		dataService = service;
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

	[RelayCommand]
	private async void ChangeElements(string category)
	{
		Console.WriteLine("dasds");
	}
}
