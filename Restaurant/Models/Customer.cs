using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<OrderVm>();
            Payment = new HashSet<Payment>();
        }

        public int CustomerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public bool? ProfileStatus { get; set; }
        public string Userid { get; set; }

        public AspNetUsers User { get; set; }
        public ICollection<OrderVm> Orders { get; set; }
        public ICollection<Payment> Payment { get; set; }
    }
}
