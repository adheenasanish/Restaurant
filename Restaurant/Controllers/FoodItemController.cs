using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Repositories;

namespace Restaurant.Controllers
{
    public class FoodItemController : Controller
    {
        private RestaurantContext db;
        private FoodItemRepo foodItemRepo;
        public FoodItemController(RestaurantContext db)
        {
            this.db = db;

        }
        public IActionResult Index()
        {
            IEnumerable<FoodItem> foodItems = foodItemRepo.GetAllFoodItems();
            return View(foodItems);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(FoodItem food)
        {
            return View();
                     
        }
    }
}