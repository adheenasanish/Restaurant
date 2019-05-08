using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Administrator = new HashSet<Administrator>();
        }

        public int MenuId { get; set; }
        public decimal? Price { get; set; }
        public int? FoodId { get; set; }

        public FoodItem Food { get; set; }
        public ICollection<Administrator> Administrator { get; set; }
    }
}
