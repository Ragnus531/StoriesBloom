using CommunityToolkit.Maui.Views;
using StoriesBloom.Views.Popups;

namespace StoriesBloom;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(StoriesDetailPage), typeof(StoriesDetailPage));
	}

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    this.ShowPopup(new LoadingStoriesPopupPage());
    //}
}
