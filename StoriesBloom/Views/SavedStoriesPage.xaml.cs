namespace StoriesBloom.Views;

public partial class SavedStoriesPage : ContentPage
{
	public SavedStoriesPage(SavedStoriesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		viewModel.InitData();

    }

}
