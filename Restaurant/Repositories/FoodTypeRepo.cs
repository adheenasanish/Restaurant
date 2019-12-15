using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Repositories
{
    public class FoodTypeRepo
    {
        private RestaurantContext db;
        public FoodTypeRepo(RestaurantContext db)
        {
            this.db = db;

        }
        public IList<FoodType>  GetAllType()
        {
            var type = db.FoodType;
            return type.ToList();
        }
    }
  

}
