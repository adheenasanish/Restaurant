using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class OrderDetails
    {
        public int OrderItemId { get; set; }
        public int? FoodId { get; set; }
        public int? OrderId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Hstgst { get; set; }
        public decimal? Pst { get; set; }

        public FoodItem Food { get; set; }
        public Orders Order { get; set; }
    }
}
