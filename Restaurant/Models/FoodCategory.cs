using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class FoodCategory
    {
        public FoodCategory()
        {
            CategoryFoodType = new HashSet<CategoryFoodType>();
            FoodType = new HashSet<FoodType>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<CategoryFoodType> CategoryFoodType { get; set; }
        public ICollection<FoodType> FoodType { get; set; }
    }
}
