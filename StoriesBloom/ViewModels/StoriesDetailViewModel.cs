using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace StoriesBloom.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class StoriesDetailViewModel : BaseViewModel
{
	[ObservableProperty]
    StoryDetail item;

    public ICommand GoBackCommand { get; }

	public StoriesDetailViewModel()
	{
        GoBackCommand = new AsyncRelayCommand(GoBack);
	}

    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
