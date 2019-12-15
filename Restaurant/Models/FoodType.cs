using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class FoodType
    {
        public FoodType()
        {
            CategoryFoodType = new HashSet<CategoryFoodType>();
            FoodItem = new HashSet<FoodItem>();
        }

        public int FoodTypeId { get; set; }
        public string TypeName { get; set; }
        public int? CategoryId { get; set; }

        public FoodCategory Category { get; set; }
        public ICollection<CategoryFoodType> CategoryFoodType { get; set; }
        public ICollection<FoodItem> FoodItem { get; set; }
    }
}
