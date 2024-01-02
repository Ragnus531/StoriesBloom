using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Services
{
    public class SavedStoryService : ISavedStoryService
    {
        private ILogger<SavedStoryService> _logger;

        public SavedStoryService(ILogger<SavedStoryService> logger)
        {
            _logger = logger;
        }

        private List<StoryDetail> _cachedSavedStories = new List<StoryDetail>();


        public void DeleteSavedStory(StoryDetail storyDetail)
        {
            var storyDetails = GetStoriesFromFile();
            
            if (!storyDetails.Any())
            {
                return;
            }

            storyDetails.RemoveAll(s => s.Title == storyDetail.Title); //In case of duplicates also
            _cachedSavedStories = storyDetails;

            SaveToFile(storyDetails);
        }

        public void SaveStory(StoryDetail storyDetail)
        {
            var storyDetails = GetStoriesFromFile();

            if (storyDetails.Any(a => a.Title == storyDetail.Title))
            {
                return;
            }

            storyDetails.Add(storyDetail);
            _cachedSavedStories = storyDetails;

            SaveToFile(storyDetails);
        }

        public IEnumerable<StoryDetail> GetAll()
        {
            if (_cachedSavedStories.Any())
            {
                return _cachedSavedStories;
            }

            var storyDetails = GetStoriesFromFile();
            _cachedSavedStories = storyDetails;
            return storyDetails;
        }

        public bool IsSaved(StoryDetail storyDetail)
        {
            var list = GetAll();
            return list.Any(s => s.Title == storyDetail.Title);
        }

        public IEnumerable<StoryDetail> RemoveAll()
        {
            var storyDetails = GetStoriesFromFile();
            SaveToFile(string.Empty);
            return storyDetails;
        }

        private string GetSavedStoriesPath() =>
            Path.Combine(FileSystem.Current.AppDataDirectory, "saved_stories.json");

        private List<StoryDetail> GetStoriesFromFile()
        {
            List<StoryDetail> storyDetails = new List<StoryDetail>();
            string path = GetSavedStoriesPath();
            try
            {
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    if (string.IsNullOrEmpty(json))
                    {
                        return storyDetails;
                    }
                    storyDetails = JsonSerializer.Deserialize<List<StoryDetail>>(json);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                storyDetails = new List<StoryDetail>(); // just make sure storyDetails is empty in case of error
            }
           
            return storyDetails;
        }

        //TODO: RETURN A BOOLEAN and show a popup that there is a error
        private void SaveToFile(List<StoryDetail> storyDetails)
        {
            try
            {
                var newJson = JsonSerializer.Serialize(storyDetails);
                File.WriteAllText(GetSavedStoriesPath(), newJson);
                var kk = File.ReadAllText(GetSavedStoriesPath());
                Console.WriteLine("dasda");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void SaveToFile(string text)
        {
            try
            {
                File.WriteAllText(GetSavedStoriesPath(), text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
