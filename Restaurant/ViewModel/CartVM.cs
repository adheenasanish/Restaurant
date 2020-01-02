using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class CartVM
    {
        public int id { get; set; }
        [DisplayName("Item Name")]
        public string Productname { get; set; }
        [DisplayName("Quantity")]
        public int? Qty { get; set; }
        [DisplayName("Unit Price")]
        public decimal unitPrice { get; set; }
        [DisplayName("Total")]
        public decimal total { get; set; }

    }
}
