#if ANDROID
using StoriesBloom.Platforms.Android;
#endif

using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using StoriesBloom.Factories;
using Microsoft.Extensions.Logging;
namespace StoriesBloom;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureMauiHandlers(handlers => {
#if ANDROID
                handlers.AddHandler<Picker, CustomPickerHandler>();
#endif
             })
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
                fonts.AddFont("Epilogue-Medium.ttf", "Epilogue");
                fonts.AddFont("fontello.ttf", "Icons");
            });

        builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<IPopupService, PopupService>();

        builder.Services.AddTransient<SampleDataService>();
        builder.Services.AddTransient<StoryDataService>();
		

		StoriesByCategoryFactory storiesByCategoryFactory = new StoriesByCategoryFactory();
		storiesByCategoryFactory.InitStories();
        builder.Services.AddSingleton<IStoriesFactory>(storiesByCategoryFactory);
        
		builder.Services.AddSingleton<StoriesDetailViewModel>();
		builder.Services.AddSingleton<StoriesDetailPage>();

		
        builder.Services.AddSingleton<StoriesViewModel>();

		builder.Services.AddSingleton<StoriesPage>();

		builder.Services.AddSingleton<SavedStoriesViewModel>();

		builder.Services.AddSingleton<ISavedStoryService, SavedStoryService>();

		builder.Services.AddSingleton<SavedStoriesPage>();

		builder.Services.AddSingleton<StoriesCategory>();
		builder.Services.AddSingleton<StoriesCategoryViewModel>();

		CategoriesService categoriesService = new CategoriesService();
		categoriesService.InitCategories();
		builder.Services.AddSingleton<ICategoriesService>(categoriesService);

        var app =  builder.Build();

		var storiesVM = app.Services.GetRequiredService<StoriesViewModel>();
		storiesVM.InitListener();

        var savedStoriesVM = app.Services.GetRequiredService<SavedStoriesViewModel>();
        savedStoriesVM.InitListener();

        var savedStoryService = app.Services.GetRequiredService<ISavedStoryService>();
		savedStoryService.RemoveAll();

        //      var savedStoriesVM = app.Services.GetRequiredService<SavedStoriesViewModel>();
        //Task.Run(async () =>
        //{
        //	await savedStoriesVM.InitData();
        //});

        return app;
	}
}
