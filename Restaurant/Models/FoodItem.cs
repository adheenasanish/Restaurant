using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class FoodItem
    {
        public FoodItem()
        {
            CartItem = new HashSet<CartItem>();
            Menu = new HashSet<Menu>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string ItemCategory { get; set; }
        public int? FoodTypeId { get; set; }

        public FoodType FoodType { get; set; }
        public ICollection<CartItem> CartItem { get; set; }
        public ICollection<Menu> Menu { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
