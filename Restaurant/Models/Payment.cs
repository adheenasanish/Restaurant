using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentType { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderId { get; set; }

        public Customer Customer { get; set; }
        public OrderVm Order { get; set; }
    }
}
