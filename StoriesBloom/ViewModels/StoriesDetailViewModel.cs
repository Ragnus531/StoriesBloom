using CommunityToolkit.Mvvm.Input;
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

    public StoriesDetailViewModel(ISavedStoryService savedStoryService)
	{
        GoBackCommand = new AsyncRelayCommand(GoBack);
        SaveStoryCommand = new RelayCommand(SaveStory);
        _savedStoryService = savedStoryService;
    }

    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    private void SaveStory()
    {
        _savedStoryService.SaveStory(Item);
        Console.WriteLine("Saved item");
    }
}
