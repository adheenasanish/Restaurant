using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class FoodItemVM
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ItemCategory { get; set; }
        [DisplayName("Food Type")]
        public int? FoodTypeId { get; set; }

        [DisplayName("Food Category")]
        public int? CategoryId { get; set; }
        public string CategoryName{ get; set; }

        public string FoodTypeName { get; set; }
        public FoodCategory FoodCategory { get; set; }
        public FoodType FoodType { get; set; }
        public ICollection<Menu> Menu { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
