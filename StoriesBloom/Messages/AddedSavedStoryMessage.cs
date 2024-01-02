using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Messages
{
    public class AddedSavedStoryMessage : ValueChangedMessage<StoryDetail>
    {
        public AddedSavedStoryMessage(StoryDetail value) : base(value)
        {

        }
    }
}
