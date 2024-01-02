using CommunityToolkit.Mvvm.Messaging.Messages;

namespace StoriesBloom.Messages
{
    public class DeletedSavedStoryMessage : ValueChangedMessage<StoryDetail>
    {
        public DeletedSavedStoryMessage(StoryDetail value) : base(value)
        {

        }
    }
}
