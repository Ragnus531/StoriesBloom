using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Services
{
    public interface ICategoriesService
    {
        List<Category> Categories { get; init; }
        void InitCategories();
    }
}
