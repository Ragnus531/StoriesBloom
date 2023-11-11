namespace StoriesBloom.Views;

public partial class StoriesPage : ContentPage
{
	StoriesViewModel ViewModel;

	public StoriesPage(StoriesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = ViewModel = viewModel;
    }

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		await ViewModel.LoadDataAsync();
	}

    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
		//flexLayout.Clear();
		//flexLayout.Add(new Label() { Text = "nnn" }); 
		//flexLayout.Add(new Label() { Text = "nnn222" });
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(StoriesDetailPage), true, new Dictionary<string, object>
        {
            { "Item", new StoryDetail("f","f","f","f","f","f","f","f","d") }
        });
    }
}
