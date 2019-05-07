using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Repositories;
using Restaurant.ViewModel;

namespace Restaurant.Controllers
{
    public class MenuController : Controller
    {
        private RestaurantContext db;
        private MenuRepo menuRepo;
        public MenuController(RestaurantContext db)
        {
            this.db = db;
            menuRepo = new MenuRepo(db);
        }

        public IActionResult Index()
        {
            IEnumerable<Menu> menuItems = menuRepo.GetAllMenuItems();
            return View(menuItems);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Create(MenuVM menuVM)
        //{
           
        //}
    }
}