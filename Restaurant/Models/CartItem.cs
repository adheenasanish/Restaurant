using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public int? ProductId { get; set; }
        public int? Qty { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CartId { get; set; }

        public ShoppingCart Cart { get; set; }
        public FoodItem Product { get; set; }
    }
}
