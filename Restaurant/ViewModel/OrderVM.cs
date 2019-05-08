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
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        [DisplayName("Quantity")]
        public int? Qty { get; set; }
        public TimeSpan? PickupTime { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }


    }
}
