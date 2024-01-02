using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using StoriesBloom.Messages;
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

        //WeakReferenceMessenger.Default.Register<AddedSavedStoryMessage>(this, (r, m) =>
        //{
        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        if (Item != null)
        //        {
        //            if (Item.Title == m.Value.Title)
        //            {
        //                Item.Show = true;
        //            }
        //        }
        //    });
           
        //});

        //WeakReferenceMessenger.Default.Register<DeletedSavedStoryMessage>(this, (r, m) =>
        //{
        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        if (Item != null)
        //        {
        //            if (Item.Title == m.Value.Title)
        //            {
        //                Item.Show = false;
        //            }
        //        }
        //    });
           
        //});
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
            WeakReferenceMessenger.Default.Send(new DeletedSavedStoryMessage(Item));
            _logger.LogInformation("Item {title} deleted from favourites!", Item.Title);
        }   
        else
        {
            _savedStoryService.SaveStory(Item);
            Item.Saved = true;
            WeakReferenceMessenger.Default.Send(new AddedSavedStoryMessage(Item));
            _logger.LogInformation("Item {title} saved to favourites!", Item.Title);
        }
        
    }
}
