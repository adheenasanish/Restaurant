using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Repositories
{
   
    public class MenuRepo
    {
        private RestaurantContext db;
        public MenuRepo(RestaurantContext db)
        {
            this.db = db;
        }

        //Get all Menu items
        public IEnumerable<Menu> GetAllMenuItems()
        {

            return db.Menu;

        }

        //Create menu
        //public bool createNew(MenuVM menuVM)
        //{


        //}

    }
}
