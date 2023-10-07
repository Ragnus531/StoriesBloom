namespace StoriesBloom.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class StoriesDetailViewModel : BaseViewModel
{
	[ObservableProperty]
    StoryDetail item;
}
