using StoriesBloom.Resources.Strings;

namespace StoriesBloom.Factories
{
    public class StoriesByCategoryFactory : IStoriesFactory
    {
        private static Dictionary<string, byte[]> storiesDict = new Dictionary<string, byte[]>();

        public void InitStories()
        {
            if(storiesDict.Count > 0)
            {
                return;
            }

            storiesDict.Add("Romance", StoriesResources.Romance);
            storiesDict.Add("Thriller", StoriesResources.Thriller);
            storiesDict.Add("Science Fiction", StoriesResources.ScienceFiction);
            storiesDict.Add("Fantasy", StoriesResources.Fantasy);
            storiesDict.Add("Religious", StoriesResources.Religious);
            storiesDict.Add("Humor", StoriesResources.Humor);
            storiesDict.Add("Historical Fiction", StoriesResources.HistoricalFiction);
            storiesDict.Add("Young Adult", StoriesResources.YoungAdult);
            storiesDict.Add("Adventure", StoriesResources.Adventure);
            storiesDict.Add("Dystopian", StoriesResources.Dystopian);
            storiesDict.Add("Horror", StoriesResources.Horror);
        }

        public byte[] GetStories(string categoryName)
        {
            return storiesDict[categoryName];
        }
    }
}
