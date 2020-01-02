using Microsoft.AspNetCore.Http;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Repositories
{
  
    public class PaypalRepo
    {
        private RestaurantContext db;
        public PaypalRepo(RestaurantContext db)
        {
            this.db = db;

        }

        //Add payement details that are coming from Paypal
        public bool AddPaypalDetails(IPN ipn, string userName)
        {
           

            string cust =Convert.ToString( ipn.custom);
            string customerId = cust.Trim('\'');

            string custom = customerId.Trim(' ').ToString();
            
                Payments payments = new Payments
                {
                    Custom = custom,  // Customer details along with orderID
                    PaymentId = ipn.paymentID,
                    Cart = ipn.cart,
                    CreateTime = ipn.create_time,
                    PayerId = ipn.payerID,
                    PayerFirstName = ipn.payerFirstName,
                    PayerMiddleName = ipn.payerMiddleName,
                    PayerEmail = ipn.payerEmail,
                    PayerCountryCode = ipn.payerCountryCode,
                    PayerStatus = ipn.payerStatus,
                    Amount = ipn.amount,
                    Currency = ipn.currency,
                    Intent = ipn.intent,
                    PayerLastName = ipn.payerLastName,
                    PaymentMethod = ipn.paymentMethod,
                    PaymentState = ipn.paymentState


                };
                db.Payments.Add(payments);
                db.SaveChanges();
            var user = db.AspNetUsers.Where(a => a.UserName == userName).FirstOrDefault();
            var details = db.Orders.Where(o => o.PayementStatus == null && o.UserId == user.Id).FirstOrDefault();

            var paymentUpdates = db.Payments.Where(p => p.Custom == userName + "|" + details.OrderId).FirstOrDefault();

            details.PayementStatus = paymentUpdates.PaymentState;

            db.Orders.Update(details);

            db.SaveChanges();

            var cart = db.ShoppingCart.Where(s => s.UserId == userName).FirstOrDefault();

            var cartDetails = db.CartItem.Where(c => c.CartId == cart.CartId);
            foreach(var item in cartDetails)
            {
                if(item != null)
                {
                    db.CartItem.Remove(item);
                   

                }
            }
            db.SaveChanges();

            db.ShoppingCart.Remove(cart);
            db.SaveChanges();     

           
            return true;
        }

        //Create an order After payment appoval
        public int CreateOrder(string userName,decimal total, IEnumerable<CartVM> result)
        {
            var user = db.AspNetUsers.Where(a => a.UserName == userName).FirstOrDefault();
            Orders orders = new Orders
            {
                Total = total,
                UserId = user.Id,
                OrderDate = DateTime.Now

            };
            db.Orders.Add(orders);
            db.SaveChanges();

            var details = db.Orders.Where(o => o.PayementStatus == null && o.UserId == user.Id).FirstOrDefault();
            
            //Add details to OrderDetails 
            foreach (var oneItem in result)
            {
                var foodItem = db.FoodItem.Where(f => f.Name == oneItem.Productname).FirstOrDefault();
                OrderDetails orderDetails = new OrderDetails
                {
                    OrderId = details.OrderId,
                    Quantity = oneItem.Qty,
                    FoodId = foodItem.FoodId

                };
                db.OrderDetails.Add(orderDetails);
                db.SaveChanges();

            }

            return details.OrderId;

        }
    }
}
