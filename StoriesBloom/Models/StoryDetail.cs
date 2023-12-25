﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Models
{
    //public record StoryDetail(string Title, string Prologue
    //                          ,string Chapter1,string Chapter2, string Chapter3, string Chapter4, string Chapter5
    //                          ,string Epilogue,string UnexpectedTwist);

    public record StoryDetail
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

        public string ImagePath
        {
            get
            {
                string strippedTitle = Title.Trim()
                    .Replace(" ", "_").Replace("..", ".").Replace(".",string.Empty)
                    .Replace("’", "").ToLower();
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
    }
}
