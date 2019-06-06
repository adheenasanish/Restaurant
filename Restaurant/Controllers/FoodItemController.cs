using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Repositories;

namespace Restaurant.Controllers
{
    public class FoodItemController : Controller
    {
        private RestaurantContext db;
        private FoodItemRepo foodItemRepo;
        private readonly IHostingEnvironment hostingEnvironment;
        public FoodItemController(RestaurantContext db, IHostingEnvironment hEnvironmnet)
        {
            this.db = db;
            hostingEnvironment = hEnvironmnet;
        }
        public IActionResult Index()
        {
            foodItemRepo = new FoodItemRepo(db);
            IEnumerable<FoodItem> foodItems = foodItemRepo.GetAllFoodItems();
            return View(foodItems);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FoodItem food,IFormFile imageUpload1)
        {
            foodItemRepo = new FoodItemRepo(db);
            bool result = false;
            string image = "";
            if (ModelState.IsValid)
            {
                if (imageUpload1 != null)
                {
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, Path.GetFileName(imageUpload1.FileName));
                    imageUpload1.CopyTo(new FileStream(fileName, FileMode.Create));

                    //ViewData["fileLocation"] = fileName;
                    image = "/" + Path.GetFileName(imageUpload1.FileName);
                    //newStay.Image = "/" + Path.GetFileName(imageUpload1.FileName);

                }
                result = foodItemRepo.CreateNew(food, image);
            }

            if(result == true)
            {
                return RedirectToAction("Index", "FoodItem");
            }
            else
            {
                return NotFound();
            }
          
           

           // return View();
                     
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            foodItemRepo = new FoodItemRepo(db);
            FoodItem details = foodItemRepo.GetDetails(id);

            return View(details);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            foodItemRepo = new FoodItemRepo(db);
            FoodItem details = foodItemRepo.GetDetails(id);
            return View(details);

        }
        [HttpPost]
        public IActionResult Edit(FoodItem food)
        {
            foodItemRepo = new FoodItemRepo(db);
            //if( food != Null)
            bool result = foodItemRepo.Update(food);
            return View();

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            foodItemRepo = new FoodItemRepo(db);
            return View();

        }
    }
}