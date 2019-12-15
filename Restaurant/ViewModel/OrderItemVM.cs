using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class OrderItemVM
    {
        public string Itemname { get; set; }      
        public decimal ItemUnitPrice { get; set; }         
  
        public int Qty { get; set; }
    }
}
