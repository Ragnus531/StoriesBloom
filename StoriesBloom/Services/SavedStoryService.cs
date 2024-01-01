using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Services
{
    public class SavedStoryService : ISavedStoryService
    {
        private List<StoryDetail> _cachedSavedStories = new List<StoryDetail>();

        public void SaveStory(StoryDetail storyDetail)
        {
            var storyDetails = new List<StoryDetail>();
            string path = GetSavedStoriesPath();
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                storyDetails = JsonSerializer.Deserialize<List<StoryDetail>>(json);
            }

            storyDetails.Add(storyDetail);
            _cachedSavedStories = storyDetails;

            var newJson = JsonSerializer.Serialize(storyDetails);
            File.WriteAllText(path, newJson);
        }

        public IEnumerable<StoryDetail> GetAll()
        {
            if (_cachedSavedStories.Any())
            {
                return _cachedSavedStories;
            }

            var storyDetails = new List<StoryDetail>();
            string path = GetSavedStoriesPath();
            try
            {
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    storyDetails = JsonSerializer.Deserialize<List<StoryDetail>>(json);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            _cachedSavedStories = storyDetails;
            return storyDetails;
        }

        public bool IsSaved(StoryDetail storyDetail)
        {
            var list = GetAll();
            return list.Any(s => s.Title == storyDetail.Title);
        }

        private string GetSavedStoriesPath() =>
            Path.Combine(FileSystem.Current.AppDataDirectory, "saved_stories.json");

    }
}
