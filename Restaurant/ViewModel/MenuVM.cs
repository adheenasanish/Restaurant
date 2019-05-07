using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class MenuVM
    {
        public decimal? Price { get; set; }
        public int? FoodId { get; set; }

        public FoodItem Food { get; set; }
    }
}
