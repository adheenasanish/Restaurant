using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            orderRepo = new OrderRepo(db);
            IEnumerable<OrderVM> orders = orderRepo.GetAllOrders();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Get all items based on the selected menu item
        public IActionResult GetItems(string selectedMenuItem )
        {
            orderRepo = new OrderRepo(db);
            IEnumerable<FoodItem> foodItems = orderRepo.GetAllItems(selectedMenuItem);
            HttpContext.Session.SetString("SessionKeyMenu", Convert.ToString(selectedMenuItem));

            List<DisplayVM> itemName = new List<DisplayVM>();
                      
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
           
            int custId = customer.CustomerId;


            bool result = false;
                if (ModelState.IsValid)
                {
                    result = orderRepo.CreateNew(display, useName);
                }

                if (result == true)
                {
                   HttpContext.Session.SetInt32("SessionKeyCart", Convert.ToInt32(1));
                }
            string menu = HttpContext.Session.GetString("SessionKeyMenu");

            return RedirectToAction("GetItems", "Order",new { selectedMenuItem = menu });


           
        }

        //Displays the cart Items
        public IActionResult DisplayCart()
        {
            orderRepo = new OrderRepo(db);
            string userName = HttpContext.User.Identity.Name;
            IEnumerable< CartVM> result = orderRepo.GetCartItems(userName);
            return View(result);
        }
        
        //Delete items from cart
        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            string userName = HttpContext.User.Identity.Name;
            orderRepo = new OrderRepo(db);
            bool result = orderRepo.DeleteItemFromCart(id);

            bool checkForIems = orderRepo.CheckForItems(userName);
            if( checkForIems == false)
            {
                HttpContext.Session.SetInt32("SessionKeyCart", Convert.ToInt32(0));
            }
            var checkCart = HttpContext.Session.GetInt32("SessionKeyCart");
            if (checkCart == 0)
            {
                return RedirectToAction("CartEmpty", "Order");
            }
            else
            {
                return RedirectToAction("DisplayCart", "Order");
            }        
           
              
                   
        }
        //Display cart Empty message
        public IActionResult CartEmpty()
        {
            return View();
        }
       
        public IActionResult Pay()
        {
            orderRepo = new OrderRepo(db);
            string userName = HttpContext.User.Identity.Name;
            IEnumerable<CartVM> result = orderRepo.GetCartItems(userName);
            return View(result);
        }


        
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

