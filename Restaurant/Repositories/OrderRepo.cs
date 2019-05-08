using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Repositories
{

    public class OrderRepo
    {
        private RestaurantContext db;
        public OrderRepo(RestaurantContext db)
        {
            this.db = db;
        }
        public IEnumerable<Orders> GetAllOrder()
        {
            return db.Orders;
        }
        //Create new order
        public bool CreateNew(Orders order,int userId)
        {
            Orders newOrder = new Orders
            {
                OrderDate = DateTime.Now,
                PickupTime = order.PickupTime,
                Qty = order.Qty,
                CustomerId = userId
            };
            db.Orders.Add(newOrder);
            db.SaveChanges();
            return true;
        }
    }
}
