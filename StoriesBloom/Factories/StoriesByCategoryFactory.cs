using StoriesBloom.Resources.Strings;

namespace StoriesBloom.Factories
{
    public class StoriesByCategoryFactory : IStoriesFactory
    {
        public byte[] GetStories(string categoryName)
        {
            byte[] story = null;

            if(categoryName == "Romance")
            {
                story = StoriesResources.Romance;
            }
            else if (categoryName == "Thriller")
            {
                story = StoriesResources.Thriller;
            }
            else if (categoryName == "Science Fiction")
            {
                story = StoriesResources.ScienceFiction;
            }
            else if (categoryName == "Fantasy")
            {
                story = StoriesResources.Fantasy;
            }
            else if (categoryName == "Religious")
            {
                story = StoriesResources.Religious;
            }
            else if (categoryName == "Humor")
            {
                story = StoriesResources.Humor;
            }
            else if (categoryName == "Historical Fiction")
            {
                story = StoriesResources.HistoricalFiction;
            }
            else if (categoryName == "Young Adult")
            {
                story = StoriesResources.YoungAdult;
            }
            else if (categoryName == "Adventure")
            {
                story = StoriesResources.Adventure;
            }
            else if (categoryName == "Dystopian")
            {
                story = StoriesResources.Dystopian;
            }
            else if (categoryName == "Horror")
            {
                story = StoriesResources.Horror;
            }

            return story;
        }
    }
}
