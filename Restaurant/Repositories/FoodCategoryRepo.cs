using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Repositories
{
    public class FoodCategoryRepo
    {
        private RestaurantContext db;
        public FoodCategoryRepo(RestaurantContext db)
        {
            this.db = db;

        }
        public IList<FoodCategory> GetAllCategory()
        {
            var categories = db.FoodCategory;
            return categories.ToList();
        }
    }
}
