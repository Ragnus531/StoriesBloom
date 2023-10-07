namespace StoriesBloom.Views;

public partial class StoriesDetailPage : ContentPage
{
	public StoriesDetailPage(StoriesDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
