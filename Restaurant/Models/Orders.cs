﻿using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Qty { get; set; }
        public TimeSpan? PickupTime { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
