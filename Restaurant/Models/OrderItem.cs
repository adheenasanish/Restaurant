using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int? FoodId { get; set; }
        public int? OrderId { get; set; }
        public decimal? Total { get; set; }
        public int? Quantity { get; set; }

        public FoodItem Food { get; set; }
        public Orders Order { get; set; }
    }
}
