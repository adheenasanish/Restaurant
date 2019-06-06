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
            var allOrders = from oi in db.OrderItem
                            from o in db.Orders
                            where o.OrderId == oi.OrderId
                            select new  { o.PickupTime, oi.OrderId ,oi.Total,oi.Quantity,oi.FoodId,o.OrderDate,o.Customer.FirstName,o.Customer.LastName};
            OrderVM itemOrders = new OrderVM();


            foreach (var items in allOrders)
             {
                //var customer = db.Customer.Where(c => c.CustomerId == items.CustomerId).FirstOrDefault();
                var food = db.FoodItem.Where(f => f.FoodId == items.FoodId).FirstOrDefault();

                itemOrders = new OrderVM
                {
                    OrderId = Convert.ToInt32(items.OrderId),
                    OrderDate = items.OrderDate,
                    Qty = items.Quantity,
                    PickupTime = items.PickupTime,
                    LastName = items.LastName,
                    FirstName = items.FirstName,
                    Name = food.Name


                };
            }

            yield return itemOrders;
        }
        //Create new order
        public bool CreateNew(OrderVM order,int userId)
        {
            OrderVM newOrder = new OrderVM
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

            //db.Orders.Add(newOrder);
            db.OrderItem.Add(orderItem);
            db.SaveChanges();
            return true;
        }

        public IEnumerable<FoodItem> GetAllItems(string selectedMenuItem)
        {
            var allItems = db.FoodItem.Where(f => f.Type == selectedMenuItem);
            return allItems;
        }

        public DisplayVM GetDetails(int id)
        {
            var details = db.FoodItem.Where(f => f.FoodId == id).FirstOrDefault();
            
            DisplayVM displayVM = new DisplayVM
            {
                ItemId = details.FoodId,
                ItemImage = details.Image,
                Itemname = details.Name,
                ItemPrice = Convert.ToDecimal(details.UnitPrice)
            };
            return (displayVM);
        }
    }
}
