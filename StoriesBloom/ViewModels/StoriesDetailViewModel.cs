using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace StoriesBloom.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class StoriesDetailViewModel : BaseViewModel
{
	[ObservableProperty]
    StoryDetail item;
    private readonly ISavedStoryService _savedStoryService;

    public ICommand GoBackCommand { get; }
    public ICommand SaveStoryCommand { get; }
    ILogger<StoriesDetailViewModel> _logger;

    public StoriesDetailViewModel(ISavedStoryService savedStoryService, ILogger<StoriesDetailViewModel> logger)
    {
        GoBackCommand = new AsyncRelayCommand(GoBack);
        SaveStoryCommand = new RelayCommand(SaveStory);
        _savedStoryService = savedStoryService;
        _logger = logger;
    }

    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    private void SaveStory()
    {
        if (Item.Saved)
        {
            _savedStoryService.DeleteSavedStory(Item);
            Item.Saved = false;
            _logger.LogInformation("Item {title} deleted from favourites!", Item.Title);
        }   
        else
        {
            _savedStoryService.SaveStory(Item);
            Item.Saved = true;
            _logger.LogInformation("Item {title} saved to favourites!", Item.Title);
        }
        
    }
}
