using StoriesBloom.Factories;
using StoriesBloom.Resources.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StoriesBloom.Services
{
    public class StoryDataService
    {
        private IStoriesFactory _storiesFactory;

        public StoryDataService(IStoriesFactory storiesFactory)
        {
            _storiesFactory = storiesFactory;
        }

        public IEnumerable<StoryDetail> GetStories(string category = "Romance")
        {
            var listToReturn = new List<StoryDetail>();
            byte[] das = _storiesFactory.GetStories(category);
            ParseList(listToReturn, das);
            return listToReturn;
        }



        public IEnumerable<StoryDetail> Get5RandomStories()
        {
            var listToReturn = new List<StoryDetail>();
            IEnumerable<byte[]> allStories = _storiesFactory.GetAllStories();
            Random rand = new Random();
            var randomStories = allStories.OrderBy(x => rand.Next()).Take(5);
            foreach (var story in randomStories)
            {
                ParseListWithLimit(listToReturn, story, 1);
            }
            return listToReturn;
        }

        private void ParseList(List<StoryDetail> listToReturn, byte[] das)
        {
            JsonParser jsonParser = new JsonParser();
            var nn = jsonParser.GetStories(das);
            foreach (var item in nn)
            {
                string title = item.Title;
                string story = item.Content;
                //var ele = ParseText(story,title);
                var ele = SplitTextIntoChapters(story, title);
                if (ele != null)
                {
                    listToReturn.Add(ele);
                }
            }
        }

        private void ParseListWithLimit(List<StoryDetail> listToReturn, byte[] das,int limit)
        {
            Random rand = new Random();
            JsonParser jsonParser = new JsonParser();
            var nn = jsonParser.GetStories(das).OrderBy(x => rand.Next()).Take(limit);
            foreach (var item in nn)
            {
                string title = item.Title;
                string story = item.Content;
                //var ele = ParseText(story,title);
                var ele = SplitTextIntoChapters(story, title);
                if (ele != null)
                {
                    listToReturn.Add(ele);
                }
            }
        }

        public StoryDetail SplitTextIntoChapters(string text,string title)
        {
            string prologue = "", chapter1 = "", chapter2 = "", chapter3 = "", chapter4 = "", chapter5 = "", epilogue = "", twistedEnding = "";

            List<string> chapters = new List<string>();
            string pattern = @"(\$[A-Za-z0-9\s]*\$)";
            string[] substrings = Regex.Split(text, pattern);
            for (int i = 1; i < substrings.Length; i += 2)
            {
                string chapterText = substrings[i + 1].Trim();
                chapters.Add(chapterText);
            }

            prologue = chapters[0]; chapter1 = chapters[1]; chapter2 = chapters[2]; chapter3 = chapters[3];
            chapter4 = chapters[4]; chapter5 = chapters[5]; epilogue = chapters[6]; twistedEnding = chapters.Count() >= 8 ? chapters[7] : string.Empty;
            return new StoryDetail(title, prologue, chapter1, chapter2, chapter3, chapter4, chapter5, epilogue, twistedEnding);
        }

        private  StoryDetail ParseText(string inputText,string title)
        {
            string prologue = "", chapter1 = "", chapter2 = "", chapter3 = "", chapter4 = "", chapter5 = "" , epilogue = "", twistedEnding = "";
            string pattern = @"(Prologue|Chapter 1|Chapter 2|Chapter 3|Chapter 4|Chapter 5|Epilogue|Twisted Ending|Unexpected Twist)\s+(.*?)(?=($Prologue$|$Chapter1$|$Chapter2$|$Chapter3$|$Chapter4$|$Chapter5$|$Epilogue$|$Twisted Ending$|$UnexpectedTwist$|$))";
            
            if (inputText == null)
                return null;

            MatchCollection matches = Regex.Matches(inputText, pattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                string sectionHeader = match.Groups[1].Value.Trim();
                string sectionText = match.Groups[2].Value.Trim();

                switch (sectionHeader)
                {
                    case "Prologue":
                        prologue = sectionText;
                        break;
                    case "Chapter 1":
                        chapter1 = sectionText;
                        break;
                    case "Chapter 2":
                        chapter2 = sectionText;
                        break;
                    case "Chapter 3":
                        chapter3 = sectionText;
                        break;
                    case "Chapter 4":
                        chapter4 = sectionText;
                        break;
                    case "Chapter 5":
                        chapter5 = sectionText;
                        break;
                    case "Epilogue":
                        epilogue = sectionText;
                        break;
                    case "Twisted Ending":
                        twistedEnding = sectionText;
                        break;
                    case "Unexpected Twist":
                        twistedEnding = sectionText;
                        break;
                }
            }

            return new StoryDetail(title, prologue, chapter1, chapter2, chapter3, chapter4, chapter5, epilogue, twistedEnding);
        }
    }
}
