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
        public bool CreateNew(DisplayVM order,string userId)
        {
           
           var itemDetails = db.FoodItem.Where(food => food.FoodId == order.ItemId).FirstOrDefault();
            int productId = itemDetails.FoodId;
            
            var cart1 = db.ShoppingCart.Where(s => s.UserId == userId).FirstOrDefault();
           
            // Create an item in ShoppingCart
            if (cart1 == null)
            {
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    UserId = userId,
                    CreateDate = DateTime.Now
                };
                db.ShoppingCart.Add(shoppingCart);
                db.SaveChanges();
                     

            }
          
                var cart = db.ShoppingCart.Where(s => s.UserId == userId).FirstOrDefault();

                // Add an item in Cart
                CartItem cartItem = new CartItem
                {
                    ProductId = productId,
                    Qty = order.Qty,
                    CartId = cart.CartId,
                    CreateDate = cart.CreateDate
                };

                db.CartItem.Add(cartItem);
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

        public IEnumerable<CartVM> GetCartItems(string userId)
        {
            var itemDetails = db.ShoppingCart.Where(s => s.UserId == userId).FirstOrDefault();

            var cartItems = db.CartItem.Where(c => c.CartId == itemDetails.CartId);
            CartVM cartVm = new CartVM();
            List<CartVM> cartVMs = new List<CartVM>();

            foreach(var item in cartItems)
            {
                var product = db.FoodItem.Where(f => f.FoodId == item.ProductId).FirstOrDefault();

                cartVm = new CartVM
                {
                    Productname = product.Name,
                    Qty = item.Qty,
                    unitPrice = Convert.ToDecimal(product.UnitPrice),
                    total = Math.Round(Convert.ToDecimal(product.UnitPrice * item.Qty), 2)
                } ;

                cartVMs.Add(cartVm);

            }        
           

            return cartVMs;
        }

        
    }
}
