using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class DisplayVM
    {
        public int ItemId { get; set; }
        public string Itemname { get; set; }
        public string ItemImage { get; set; }
        public decimal ItemPrice { get; set; }
        public string Type { get; set; }    
        public decimal Total { get; set; }
        public int Qty { get; set; }

    }
}
