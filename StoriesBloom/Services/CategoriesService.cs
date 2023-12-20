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
            Categories.Add(new Category() { ProfileImage = "romance.jpg", Name = "Romance", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "thriller.jpg", Name = "Thriller", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "sciencefiction.jpg", Name = "Science Fiction", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "fantasy.jpg", Name = "Fantasy", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "religious.jpg", Name = "Religious", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "humor.jpg", Name = "Humor", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "historicalfiction.jpg", Name = "Historical Fiction", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "youngadult.jpg", Name = "Young Adult", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "adventure.jpg", Name = "Adventure", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "dystopian.jpg", Name = "Dystopian", StoriesAmount = new Random().Next(1, 100)});
            Categories.Add(new Category() { ProfileImage = "horror.jpg", Name = "Horror", StoriesAmount = new Random().Next(1, 100)});
        }

    }
}
