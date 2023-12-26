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

    public void SwitchtoTab(int tabIndex)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            switch (tabIndex)
            {
                case 0: shelltabbar.CurrentItem = mainPage; break;
                case 1: shelltabbar.CurrentItem = stories; break;
                case 2: shelltabbar.CurrentItem = saved; break;
                case 3: shelltabbar.CurrentItem = settings; break;
            };
        });
    }

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    this.ShowPopup(new LoadingStoriesPopupPage());
    //}
}
