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
            return View();
        }
    }
}