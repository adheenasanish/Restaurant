using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Restaurant.Models
{
    public partial class Orders
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        [DisplayName("Quantity")]
        public int? Qty { get; set; }
        public TimeSpan? PickupTime { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
