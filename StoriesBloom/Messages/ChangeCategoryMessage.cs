using CommunityToolkit.Mvvm.Messaging.Messages;

namespace StoriesBloom.Messages
{
    public class ChangedCategoryMessage : ValueChangedMessage<string>
    {
        public ChangedCategoryMessage(string value) : base(value)
        {

        }
    }
}
