using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Repositories;

namespace Restaurant.Controllers
{
    public class OrderController : Controller
    {
        private RestaurantContext db;
        private OrderRepo orderRepo;

        public OrderController(RestaurantContext db)
        {
            this.db = db;
            orderRepo = new OrderRepo(db);
        }
        public IActionResult Index()
        {
            IEnumerable<Orders> orders = orderRepo.GetAllOrder();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Orders order)
        {
            string userName = HttpContext.User.Identity.Name;
            var customer = db.Customer.Where(c => c.Userid == userName).FirstOrDefault();
            int custId = customer.CustomerId;
            bool result = false;
            if(ModelState.IsValid)
            {
                result = orderRepo.CreateNew(order, custId);
            }
            if(result == true)
            {
                return RedirectToAction("Index", "Order");
            }
            else
            {
                return NotFound();
            }
        }

    }
}