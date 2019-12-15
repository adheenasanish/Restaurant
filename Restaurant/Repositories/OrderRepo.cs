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
        public bool CreateNew(DisplayVM order,int userId)
        {
            decimal GstOrHst = 8;
            decimal PST = 0;
           // Orders newOrder = new Orders
           // {
            //    OrderDate = DateTime.Now,
            //    // PickupTime = order.PickupTime,
            //    Qty = order.Qty,
            //    CustomerId = userId
            //};
            //db.Orders.Add(newOrder);
            //db.SaveChanges();
            //int id = newOrder.OrderId;
            var itemDetails = db.FoodItem.Where(food => food.FoodId == order.ItemId).FirstOrDefault();
           // decimal price = Convert.ToDecimal(itemDetails.UnitPrice);
            decimal total =Convert.ToDecimal( order.Qty) * order.ItemPrice;
            DateTime dt = DateTime.Now;
            var foodTypeDetails = db.FoodType.Where(ftd => ftd.FoodTypeId == itemDetails.FoodTypeId).FirstOrDefault();
            var categoryDetails = db.FoodCategory.Where(cd => cd.CategoryId == foodTypeDetails.CategoryId).FirstOrDefault();

            // var orderedItem = db.Orders.Where(o => o.OrderDate == dt && o.Qty == order.Qty && o.CustomerId == userId ).FirstOrDefault();

            OrderItem orderItem = new OrderItem
            {
                //OrderId = id,
                FoodId = order.ItemId,
                Quantity = order.Qty,
                Total = total
                //Hstgst = total * (GstOrHst / 100),
                //Pst = total * PST


            };
            db.OrderItem.Add(orderItem);
            db.SaveChanges();
            return true;
        }

        public IEnumerable<FoodItem> GetAllItems(string selectedMenuItem)
        {
            // var allItems = db.FoodItem.Where(f => f.Type == selectedMenuItem);
            var allItems = db.FoodItem.Where(f => f.FoodId != 0 );
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

        public IEnumerable<OrderItemVM> GetAllOrderItems()
        {
            var allOrderItem = db.OrderItem.Where(oi => oi.OrderId != 0 );
            OrderItemVM orderItemVM = new OrderItemVM();
            foreach(var item in allOrderItem)
            {
                var ids = db.FoodItem.Where(fi => fi.FoodId == item.FoodId).FirstOrDefault();
                orderItemVM = new OrderItemVM
                {
                    Itemname = ids.Name,
                    ItemUnitPrice = Convert.ToDecimal(ids.UnitPrice),
                    Qty = Convert.ToInt32(item.Quantity)

                };

            }

            yield return orderItemVM;
        }

        
    }
}
