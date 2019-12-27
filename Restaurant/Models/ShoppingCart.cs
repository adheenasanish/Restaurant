using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            CartItem = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public string UserId { get; set; }
        public DateTime? CreateDate { get; set; }

        public ICollection<CartItem> CartItem { get; set; }
    }
}
