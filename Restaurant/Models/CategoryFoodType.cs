using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class CategoryFoodType
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? TypeId { get; set; }

        public FoodCategory Category { get; set; }
        public FoodType Type { get; set; }
    }
}
