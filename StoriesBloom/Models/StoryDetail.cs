using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StoriesBloom.Models
{
    //public record StoryDetail(string Title, string Prologue
    //                          ,string Chapter1,string Chapter2, string Chapter3, string Chapter4, string Chapter5
    //                          ,string Epilogue,string UnexpectedTwist);

    public class StoryDetail : ObservableObject
    {
        public string Title { get; set; }
        public string Prologue { get; set; }
        public string Chapter1 { get; set; }
        public string Chapter2 { get; set; }
        public string Chapter3 { get; set; }
        public string Chapter4 { get; set; }
        public string Chapter5 { get; set; }
        public string Epilogue { get; set; }
        public string UnexpectedTwist { get; set; }

        public bool Saved { get => saved; set => SetProperty(ref saved, value); }
        public bool SavedWithoutNotification { get => saved; set => saved = value; }
        private bool saved = false;

        public bool Show { get => show; set => SetProperty(ref show, value); }
        private bool show = true;

        public string ImagePath
        {
            get
            {
                var regex = "[^0-9A-Za-z\\-_ ]";

                var strippedTitle = Regex.Replace(Title.Trim(), regex, string.Empty)
                                   .Replace(" ","_").Replace("-","_");

                //string strippedTitle = Title.Trim()
                //    .Replace(" ", "_").Replace("..", ".").Replace(".", string.Empty)
                //    .Replace("’", "").Replace("-", "_")
                //    .ToLower();
                return "/stories/" + strippedTitle + ".jpg";
            }
        }

        public StoryDetail(string title, string prologue, string chapter1, string chapter2, string chapter3, string chapter4, string chapter5, string epilogue, string unexpectedTwist)
        {
            Title = title;
            Prologue = prologue;
            Chapter1 = chapter1;
            Chapter2 = chapter2;
            Chapter3 = chapter3;
            Chapter4 = chapter4;
            Chapter5 = chapter5;
            Epilogue = epilogue;
            UnexpectedTwist = unexpectedTwist;
        }

        public StoryDetail()
        {
        }
    }
}
