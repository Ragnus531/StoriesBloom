using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Factories
{
    public interface IStoriesFactory
    {
        void InitStories();
        byte[] GetStories(string categoryName);
    }
}
