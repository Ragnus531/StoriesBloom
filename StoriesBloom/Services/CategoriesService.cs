using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom.Services
{
    public class CategoriesService : ICategoriesService
    {
        public List<Category> Categories { get; init; }

        public CategoriesService()
        {
            Categories = new List<Category>();    
        }

        public void InitCategories()
        {
            //TODO: Change logic to init StoriesAmount on application launch
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Romance", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Thriller", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Science Fiction", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Fantasy", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Religious", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Humor", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Historical Fiction", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Young Adult", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Adventure", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Dystopian", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "snow.jpg", Name = "Horror", StoriesAmount = new Random().Next(1, 100)});
        }

    }
}
