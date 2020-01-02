using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Restaurant.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Restaurant.Repositories
{

    public class OrderRepo
    {
        private RestaurantContext db;
        public OrderRepo(RestaurantContext db)
        {
            this.db = db;
        }

        public IEnumerable<OrderVM> GetAllOrders()
        {
            var orders = db.Orders;

            OrderVM orderVM = new OrderVM();

            // details;
            foreach (var order in orders )
            {
                int id = order.OrderId;
               
                var user = db.Customer.Where(c => c.UserId == order.UserId).FirstOrDefault();
                var orderDetails = db.OrderDetails.Where(od => od.OrderId == id);

                foreach(var details in orderDetails)
                {
                    var foodDetails = db.FoodItem.Where(fi => fi.FoodId == details.FoodId).FirstOrDefault();
                     orderVM = new OrderVM
                    {
                        OrderId = id,
                       OrderDate = order.OrderDate,                      
                       LastName = user.LastName,
                       FirstName = user.FirstName,
                       Name = foodDetails.Name,
                       Qty = details.Quantity,
                       Total = Convert.ToDecimal(order.Total)
                     };
                    yield return orderVM;
                }
            }

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

        //Get all Item based on menu
        public IEnumerable<FoodItem> GetAllItems(string selectedMenuItem)
        {
           

            if (selectedMenuItem == "Other")
            {
                var foodItem = db.FoodItem.Where(fi => fi.ItemCategory == "Other");
                return foodItem;
            }
          
           var foodItems = db.FoodItem.Where(fi => fi .ItemCategory == selectedMenuItem);
            return foodItems;
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
                    id = item.CartItemId,
                    Productname = product.Name,
                    Qty = item.Qty,
                    unitPrice = Convert.ToDecimal(product.UnitPrice),
                    total = Math.Round(Convert.ToDecimal(product.UnitPrice * item.Qty), 2)
                } ;

                cartVMs.Add(cartVm);

            }        
           

            return cartVMs;
        }

        //Delete an item from Cart
        public bool DeleteItemFromCart(int id)
        {

            var item = db.CartItem.Where(c => c.CartItemId == id).FirstOrDefault();
            if(item != null)
            {
                db.CartItem.Remove(item);
                db.SaveChanges();
            }
            return true;

        }

        //Check if there is any Item in the Cart
        public bool CheckForItems(string userName)
        {
            var userIdFromShoppingCart = db.ShoppingCart.Where(s => s.UserId == userName).FirstOrDefault();

            var result = db.CartItem.Where(c => c.CartId == userIdFromShoppingCart.CartId);
            int count = 0;
            
            foreach(var item in result)
            {
                if(item != null)
                {
                    count++;
                }
            }
            if(count != 0)
            {
                return true;
            }
                return false;
        }


    }
}
