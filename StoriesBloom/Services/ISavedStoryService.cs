using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Services
{
    public interface ISavedStoryService
    {
        void SaveStory(StoryDetail storyDetail);
        void DeleteSavedStory(StoryDetail storyDetail);
        IEnumerable<StoryDetail> GetAll();
        bool IsSaved(StoryDetail storyDetail);
    }
}
