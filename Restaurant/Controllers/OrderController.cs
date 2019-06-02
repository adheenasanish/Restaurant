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
    public class OrderController : Controller
    {
        private RestaurantContext db;
        private OrderRepo orderRepo;

        public OrderController(RestaurantContext db)
        {
            this.db = db;
           
        }
        public IActionResult Index()
        {
            IEnumerable<OrderVM> orders = orderRepo.GetAllOrder();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult GetItems(string selectedMenuItem)
        {
            orderRepo = new OrderRepo(db);
            IEnumerable<FoodItem> foodItems = orderRepo.GetAllItems(selectedMenuItem);
          
            List<DisplayVM> itemName = new List<DisplayVM>();
          
            //string[,] viewArray = new string[itemName, itemImage] {" ",""} ;
            //int count = itemName.Count;
            
            foreach (FoodItem foodItem in foodItems)
            {
                string itemType = foodItem.Name;
                string itemImage = foodItem.Image;
                decimal price =Convert.ToDecimal( foodItem.UnitPrice);
                DisplayVM displayVM = new DisplayVM
                {
                    Itemname = itemType,
                    ItemImage = itemImage,
                    ItemPrice = price

                };
                itemName.Add(displayVM);
            }
            //itemName.Add(itemName.)
            return View(itemName);
        }
        //[HttpGet]
        //public IActionResult showAll()
        //{
        //    orderRepo = new OrderRepo(db);
        //    IEnumerable<FoodItem> foodItems = orderRepo.GetAllItems();
        //    return View(foodItems);
           
           
        //}
        //[HttpPost]
        //public IActionResult Create(OrderVM order)
        //{
           // string userName = HttpContext.User.Identity.Name;
           //// var customer = db.Customer.Where(c => c.Userid == userName).FirstOrDefault();
           //// int custId = customer.CustomerId;
           // bool result = false;
           // if(ModelState.IsValid)
           // {
           //     result = orderRepo.CreateNew(order, custId);
           // }
           // if(result == true)
           // {
           //     return RedirectToAction("Index", "Order");
           // }
           // else
           // {
           //     return NotFound();
           // }
        //}

    }
}