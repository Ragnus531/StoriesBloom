using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Models
{
    public record StoryDetail(string Title, string Prologue
                              ,string Chapter1,string Chapter2, string Chapter3, string Chapter4, string Chapter5
                              ,string Epilogue,string UnexpectedTwist);
}
