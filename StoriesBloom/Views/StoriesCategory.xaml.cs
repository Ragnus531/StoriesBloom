namespace StoriesBloom.Views;

public partial class StoriesCategory : ContentPage
{
    StoriesCategoryViewModel ViewModel;

    public StoriesCategory(StoriesCategoryViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = ViewModel = viewModel;
    }
}