using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.Repositories;
using Restaurant.ViewModel;

namespace Restaurant.Controllers
{
    public class PaypalController : Controller
    {
        private RestaurantContext db;

        private PaypalRepo paypalRepo;
        private OrderRepo orderRepo;
        public PaypalController(RestaurantContext db)
        {
            this.db = db;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pay()
        {
            orderRepo = new OrderRepo(db);
            paypalRepo = new PaypalRepo(db);
            decimal total = 0;
            string userName = HttpContext.User.Identity.Name;
           
            IEnumerable <CartVM> result = orderRepo.GetCartItems(userName);
            
            foreach(var oneItem in result)
            {
                total = total + oneItem.total;
            }
            ViewBag.TotalPrice = total;
            int orderId = paypalRepo.CreateOrder(userName,total,result);
            ViewBag.userId = userName+"|"+orderId ;


            return View(result);
        }


        [HttpPost]
        public JsonResult PaySuccess([FromBody]IPN ipn)
        {
            string userName = HttpContext.User.Identity.Name;
            try
            {
                paypalRepo = new PaypalRepo(db);
                bool result = paypalRepo.AddPaypalDetails(ipn, userName);
                if( result == true)
                {
                    HttpContext.Session.SetInt32("SessionKeyCart", Convert.ToInt32(0));
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return Json(ipn);
        }



        public IActionResult FinishShopping(string paymentID)
        {
            Payments transaction = db.Payments.Where(i => i.PaymentId == paymentID).FirstOrDefault();
            return View();
        }
    }
}