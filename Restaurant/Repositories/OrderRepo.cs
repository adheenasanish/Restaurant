using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Restaurant.ViewModel;
using System;
using System.Collections;
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
        public IEnumerable<OrderVM> GetAllOrder()
        {
           var allOrders = db.Orders.Include(o => o.OrderItem).Where(o => o.OrderId != 0).ToList();
            //var allOrderss = db.Orders.Where(o => db.OrderItem.Any(oi => oi.OrderId == o.OrderId)).Select(
            //    o => new
            //    {
            //     OrderId = o.OrderId



            //    }).Select(oi => new
            //    {
            //      OrderItemId = oi.
            //    });
                //Where(o => o.OrderId != 0).ToList();
            foreach (var items in allOrders)
             {
                var customer = db.Customer.Where(c => c.CustomerId == items.CustomerId).FirstOrDefault();
                //var food = db.FoodItem.Where()

                OrderVM itemOrders = new OrderVM
                {
                    OrderId = items.OrderId,
                    OrderDate = items.OrderDate,
                    Qty = items.Qty,
                    PickupTime = items.PickupTime,
                    LastName = customer.LastName,
                    FirstName = customer.FirstName,
                   // FoodId = items.t
                    

                };
            }

            OrderVM orders = new OrderVM
            {
                //OrderId = allOrders.

            };
            return orders;
        }
        //Create new order
        public bool CreateNew(OrderVM order,int userId)
        {
            OrderVm newOrder = new OrderVm
            {
                OrderDate = DateTime.Now,
                PickupTime = order.PickupTime,
                Qty = order.Qty,
                CustomerId = userId
            };

            var itemDetails = db.FoodItem.Where(food => food.FoodId == order.FoodId).FirstOrDefault();
            decimal price = Convert.ToDecimal(itemDetails.UnitPrice);
            decimal total =Convert.ToDecimal( order.Qty) * price;

           OrderItem orderItem = new OrderItem
            {
                OrderId = order.OrderId,
                FoodId = order.FoodId,
                Quantity = order.Qty,
                Total = total

            };

            db.Orders.Add(newOrder);
            db.OrderItem.Add(orderItem);
            db.SaveChanges();
            return true;
        }
    }
}
