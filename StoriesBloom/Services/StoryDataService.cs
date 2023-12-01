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

        public async Task<IEnumerable<StoryDetail>> GetStories(string category = "Romance")
        {
            var listToReturn = new List<StoryDetail>();
            var das = _storiesFactory.GetStories(category);


            JsonParser jsonParser = new JsonParser();
            var nn = jsonParser.GetStories(das);
            foreach (var item in nn)
            {
                string title = item.Title;
                string story = item.Content;
                //var ele = ParseText(story,title);
                var ele = SplitTextIntoChapters(story,title);
                if (ele != null)
                {
                    listToReturn.Add(ele);
                }
            }
            return listToReturn;

            //using (MemoryStream ms = new MemoryStream(das))
            //{
            //    // Open the MemoryStream with a StreamReader.
            //    using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
            //    {
            //        string line;

            //        // Read the MemoryStream line by line.
            //        while ((line = sr.ReadLine()) != null)
            //        {
            //            var splittedText = line.Split('\t');
            //            if (splittedText.Length <= 1)
            //                continue;

            //            string title = splittedText[0];

            //            string story = splittedText[1];
            //            ParseText(story);


            //            if (splittedText.Count() > 1)
            //            {
            //                listToReturn.Add(new StoryDetail(splittedText[0], splittedText[0], splittedText[0], splittedText[0], splittedText[0], splittedText[0], splittedText[0], splittedText[0], splittedText[0]));
            //            }
            //            // Add each line to the list.
            //            //lines.Add(line);
            //        }
            //    }
            //}

            //Console.WriteLine("ygyugyug");

            //return listToReturn;
            //    var lines = mm.Split(
            //    new string[] { Environment.NewLine },
            //    StringSplitOptions.None
            //).ToList();
            //		lines.RemoveAt(0);

            //		foreach ( var line in lines )
            //		{
            //            var values = line.Split(',');
            //			if(values.Count() >= 2)
            //				listToReturn.Add(new SampleItem { Title = values[0], Description = values[1] });
            //        }


            //var random = new Random().Next();

            //var result = new List<SampleItem>();

            //for (var i = 0; i < 40; i++)
            //{
            //    result.Add(new SampleItem
            //    {
            //        Title = $"Item {random}-{i}",
            //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Magna etiam tempor orci eu. Proin libero nunc consequat interdum varius. Vitae congue mauris rhoncus aenean vel elit. Ipsum dolor sit amet consectetur adipiscing elit pellentesque. Pellentesque habitant morbi tristique senectus et netus. Tempus quam pellentesque nec nam aliquam sem et. Mollis nunc sed id semper risus in hendrerit gravida rutrum. Leo vel orci porta non. Interdum velit laoreet id donec ultrices. Nulla facilisi cras fermentum odio. Nulla at volutpat diam ut. Aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque. Consectetur lorem donec massa sapien faucibus et molestie ac feugiat. Mauris nunc congue nisi vitae. Consequat id porta nibh venenatis cras. Malesuada fames ac turpis egestas integer eget. Pharetra sit amet aliquam id diam maecenas ultricies.\r\n\r\nHendrerit dolor magna eget est lorem ipsum dolor sit amet. Et pharetra pharetra massa massa ultricies mi quis. Felis bibendum ut tristique et egestas quis ipsum suspendisse. Enim sed faucibus turpis in eu mi bibendum neque. Eget nulla facilisi etiam dignissim diam quis enim. Nisl condimentum id venenatis a condimentum vitae sapien pellentesque. Id aliquet lectus proin nibh nisl condimentum id. Et molestie ac feugiat sed. Fermentum posuere urna nec tincidunt. Eget felis eget nunc lobortis. Ut lectus arcu bibendum at varius vel. In cursus turpis massa tincidunt dui. Aliquam etiam erat velit scelerisque in dictum non consectetur a. Condimentum mattis pellentesque id nibh. Ridiculus mus mauris vitae ultricies leo. Et malesuada fames ac turpis egestas integer eget. Vitae tortor condimentum lacinia quis vel eros donec ac. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Sed turpis tincidunt id aliquet risus feugiat in ante."
            //    });
            //}

            //return result;
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
