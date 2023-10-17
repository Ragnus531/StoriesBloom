namespace StoriesBloom.Views;

public partial class MainPage : ContentPage
{
	MainViewModel ViewModel;

    public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = ViewModel = viewModel;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await ViewModel.LoadDataAsync();
    }
}
