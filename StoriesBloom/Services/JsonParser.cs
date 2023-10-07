using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoriesBloom.Services
{
    public class JsonParser
    {
        public List<Story> GetStories(byte[] jsonBytes)
        {
            var jsonString = System.Text.Encoding.UTF8.GetString(jsonBytes);
            return JsonSerializer.Deserialize<List<Story>>(jsonString);
        }
    }


    public class Story
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
