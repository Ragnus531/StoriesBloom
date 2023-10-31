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
		flexLayout.Clear();
		flexLayout.Add(new Label() { Text = "nnn" }); 
		flexLayout.Add(new Label() { Text = "nnn222" });
    }
}
