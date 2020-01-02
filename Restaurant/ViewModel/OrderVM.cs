using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class OrderVM
    {
        [DisplayName("Order No")]
        public int OrderId { get; set; }
        [DisplayName("Order date")]
        public DateTime? OrderDate { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [DisplayName("Food name")]
        public string Name { get; set; }
        
        [DisplayName("Quantity")]
        public int? Qty { get; set; }
        public decimal Total { get; set; }
        public TimeSpan? PickupTime { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int FoodId { get; set; }
      
      
       
       


    }
}
