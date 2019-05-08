using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class OrderVm
    {
        public OrderVm()
        {
            OrderItem = new HashSet<OrderItem>();
            Payment = new HashSet<Payment>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Qty { get; set; }
        public TimeSpan? PickupTime { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
        public ICollection<Payment> Payment { get; set; }
    }
}
