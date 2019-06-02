using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class FoodItem
    {
        public FoodItem()
        {
            Menu = new HashSet<Menu>();
            OrderItem = new HashSet<OrderItem>();
        }

        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Type { get; set; }
        public string ItemCategory { get; set; }

        public ICollection<Menu> Menu { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
