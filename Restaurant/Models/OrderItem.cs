using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class OrderItem
    {
        public int? OrderId { get; set; }
        public int? FoodId { get; set; }
        public decimal? Total { get; set; }
        public int? Quantity { get; set; }
        public int OrderItemId { get; set; }

        public FoodItem Food { get; set; }
        public OrderVm Order { get; set; }
    }
}
