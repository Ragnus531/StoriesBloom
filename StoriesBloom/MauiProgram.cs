namespace StoriesBloom;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("FontAwesome6FreeBrands.otf", "FontAwesomeBrands");
				fonts.AddFont("FontAwesome6FreeRegular.otf", "FontAwesomeRegular");
				fonts.AddFont("FontAwesome6FreeSolid.otf", "FontAwesomeSolid");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("NotoSerif-Bold.ttf", "NotoSerifBold");
                fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemibold");
                fonts.AddFont("Poppins-Regular.ttf", "Poppins");
                fonts.AddFont("MaterialIconsOutlined-Regular.otf", "Material");
            });

		builder.Services.AddSingleton<MainViewModel>();

		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddTransient<SampleDataService>();
        builder.Services.AddTransient<StoryDataService>();
		builder.Services.AddTransient<StoriesDetailViewModel>();
		builder.Services.AddTransient<StoriesDetailPage>();

		builder.Services.AddSingleton<StoriesViewModel>();

		builder.Services.AddSingleton<StoriesPage>();

		builder.Services.AddSingleton<LocalizationViewModel>();

		builder.Services.AddSingleton<LocalizationPage>();

		return builder.Build();
	}
}
