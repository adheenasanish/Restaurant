using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Repositories
{
    public class FoodItemRepo
    {
        private RestaurantContext db;

        public FoodItemRepo(RestaurantContext db)
        {
            this.db = db;
        }

        //Get all Food items
        public IEnumerable<FoodItem> GetAllFoodItems()
        {

            return db.FoodItem;

        }

        //Create new Item
        //come
        public bool CreateNew(FoodItem food)
        {
            FoodItem NewFoodItem = new FoodItem
            {
                    Name =  food.Name,
                    Image = food.Image,
                    Quantity = food.Quantity,
                    UnitPrice =food.UnitPrice,
                    ItemCategory = food.ItemCategory
            };
            db.FoodItem.Add(NewFoodItem);
           return true;
        }

    }
}
