using Microsoft.Maui.Controls.Shapes;
using StoriesBloom.Resources.Strings;
using System.Collections;
using System.Reflection;
using System.Text;

namespace StoriesBloom.Services;

public class SampleDataService
{
    public async Task<IEnumerable<SampleItem>> GetItems()
    {
        //await Task.Delay(1000); // Artifical delay to give the impression of work

        var listToReturn = new List<SampleItem>();
        var das = StoriesResources.Romance;

        using (MemoryStream ms = new MemoryStream(das))
        {
            // Open the MemoryStream with a StreamReader.
            using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
            {
                string line;

                // Read the MemoryStream line by line.
                while ((line = sr.ReadLine()) != null)
                {
                    var splittedText = line.Split('\t');
                    if(splittedText.Count() > 1)
                    {
                        listToReturn.Add(new SampleItem()
                        {
                            Title = splittedText[0],Description = splittedText[1]
                        });
                    }
                    // Add each line to the list.
                    //lines.Add(line);
                }
            }
        }

        Console.WriteLine("ygyugyug");

        return listToReturn;
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
}
