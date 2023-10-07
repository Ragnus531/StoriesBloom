namespace StoriesBloom;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(StoriesDetailPage), typeof(StoriesDetailPage));
	}
}
