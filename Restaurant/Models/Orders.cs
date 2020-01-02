using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string PayementStatus { get; set; }
        public decimal? Total { get; set; }
        public string UserId { get; set; }
        public DateTime? OrderDate { get; set; }

        public Customer Customer { get; set; }
        public AspNetUsers User { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
