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
                string foodCategory = foodItem.ItemCategory;
                var foodTypeEntry = db.FoodType.Where(ft => ft.FoodTypeId == foodItem.FoodTypeId).FirstOrDefault();
                string foodType = foodTypeEntry.TypeName;

                DisplayVM displayVM = new DisplayVM
                {
                    Itemname = itemType,
                    ItemImage = itemImage,
                    ItemPrice = price,
                    ItemId = foodItem.FoodId,
                    Type = foodType,
                    Category = foodCategory

                };
                itemName.Add(displayVM);
            }
            //itemName.Add(itemName.)
            return View(itemName);
        }

        [HttpGet]
        public IActionResult ItemDetails(int id)
        {
            orderRepo = new OrderRepo(db);
            DisplayVM fItem = orderRepo.GetDetails(id);

            return View(fItem);
        }

        [HttpPost]
        public IActionResult ItemDetails(DisplayVM display)
        {
            orderRepo = new OrderRepo(db);
            string useName = HttpContext.User.Identity.Name;
            var customer = db.Customer.Where(c => c.Email == useName).FirstOrDefault();
            //if (customer.CustomerId == 0)
            //{
            //    return ViewBag.data = null;
            //}
            int custId = customer.CustomerId;


            bool result = false;
                if (ModelState.IsValid)
                {
                    result = orderRepo.CreateNew(display, useName);
                }

                if (result == true)
                {
                    //return 
                }
                //DisplayVM fItem = orderRepo.GetDetails(id);
                return RedirectToAction("GetItems", "Order");


           
        }

        public IActionResult DisplayCart()
        {
            orderRepo = new OrderRepo(db);
            string userName = HttpContext.User.Identity.Name;
            IEnumerable< CartVM> result = orderRepo.GetCartItems(userName);
            return View(result);
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
        [HttpGet]
        public IActionResult addtoCart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addToCart(DisplayVM display)
        {
            return View();
        }

    }
}

// public IActionResult addToCart(int id,string itemname,string type,decimal itemPrice,decimal total,int qty)