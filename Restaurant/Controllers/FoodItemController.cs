using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models;
using Restaurant.Repositories;
using Restaurant.ViewModel;

namespace Restaurant.Controllers
{
    public class FoodItemController : Controller
    {
        private RestaurantContext db;
        private FoodItemRepo foodItemRepo;
        private FoodTypeRepo foodTypeRepo;
        private FoodCategoryRepo foodCategoryRepo;
        private readonly IHostingEnvironment hostingEnvironment;
        public FoodItemController(RestaurantContext db, IHostingEnvironment hEnvironmnet)
        {
            this.db = db;
            hostingEnvironment = hEnvironmnet;
        }
        public IActionResult Index()
        {
            foodItemRepo = new FoodItemRepo(db);
            foodTypeRepo = new FoodTypeRepo(db);
            foodCategoryRepo = new FoodCategoryRepo(db);
            IEnumerable<FoodItem> foodItems = foodItemRepo.GetAllFoodItems();
            return View(foodItems);
        }

        [HttpGet]
        public IActionResult Create()
        {
           // ViewData["Type"] = db.FoodType;

            IList<FoodType> foodTypeList = new List<FoodType>();
            IList<FoodCategory> foodCategoryList = new List<FoodCategory>();

            foodTypeRepo = new FoodTypeRepo(db);
            foodCategoryRepo = new FoodCategoryRepo(db);

            foodTypeList = foodTypeRepo.GetAllType();
            foodCategoryList = foodCategoryRepo.GetAllCategory();
            //var 
             var newFoodCategory = foodCategoryList.Select(fc => new SelectListItem { Value = (fc.CategoryId).ToString(), Text = fc.CategoryName }).ToList();
            var categoryList = new SelectList(newFoodCategory, "Value", "Text");
            ViewBag.CategoryListss = categoryList;

            var newFoodType = foodTypeList.Select(f => new SelectListItem { Value = (f.FoodTypeId).ToString(), Text = f.TypeName }).ToList();
            var typeList = new SelectList(newFoodType, "Value", "Text");
            ViewBag.TypeLists = typeList;          


            // ViewData["foodTypeLists"] = new SelectList(db.FoodType, "TypeName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(FoodItem food,IFormFile imageUpload1)
        {
            foodItemRepo = new FoodItemRepo(db);
            bool result = false;
            string image = "";
            //if (ModelState.IsValid)
            //{
                if (imageUpload1 != null)
                {
                    var fileName = Path.Combine(hostingEnvironment.WebRootPath, Path.GetFileName(imageUpload1.FileName));
                    imageUpload1.CopyTo(new FileStream(fileName, FileMode.Create));
                    //ViewData["TypeName"] = 

                    //ViewData["fileLocation"] = fileName;
                    image = "/" + Path.GetFileName(imageUpload1.FileName);
                    //newStay.Image = "/" + Path.GetFileName(imageUpload1.FileName);

                }
                result = foodItemRepo.CreateNew(food, image);
         //   }

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
            IList<FoodType> foodTypeList = new List<FoodType>();
            IList<FoodCategory> foodCategoryList = new List<FoodCategory>();
            IList<FoodCategory> cate = new List<FoodCategory>();
            foodTypeRepo = new FoodTypeRepo(db);
            foodCategoryRepo = new FoodCategoryRepo(db);

            foodTypeList = foodTypeRepo.GetAllType();
            foodCategoryList = foodCategoryRepo.GetAllCategory();
            //var 
            var newFoodCategory = foodCategoryList.Select(fc => new SelectListItem { Value = (fc.CategoryId).ToString(), Text = fc.CategoryName }).ToList();
            var categoryList = new SelectList(newFoodCategory, "Value", "Text");
            ViewBag.CategoryListss = categoryList;

            var newFoodType = foodTypeList.Select(f => new SelectListItem { Value = (f.FoodTypeId).ToString(), Text = f.TypeName }).ToList();
            var typeList = new SelectList(newFoodType, "Value", "Text");
            ViewBag.TypeLists = typeList;

            foodItemRepo = new FoodItemRepo(db);
            FoodItem details = foodItemRepo.GetDetails(id);

            var categDetails = db.FoodCategory.Where(fcd => fcd.CategoryName == details.ItemCategory).FirstOrDefault();

            var selectedCategoryName =new SelectListItem { Value = (categDetails.CategoryId).ToString(), Text = categDetails.CategoryName };

           // var selectedCategory = new SelectList(new SelectListItem { Value = categDetails.CategoryId.ToString(), Text = categDetails.CategoryName }, "Value", "Text");
            ViewData["categoryResult"] = (new SelectListItem { Value = categDetails.CategoryId.ToString(), Text = categDetails.CategoryName }, "Value", "Text"); ;
            var type = db.FoodType.Where(ft => ft.FoodTypeId == details.FoodTypeId).FirstOrDefault();

            ViewData["itemType"] = type.TypeName;
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
        [HttpGet, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
           
            if( id ==  null)
            {
                return NotFound();
            }
           
              var  entry =  db.FoodItem.Where(f => f.FoodId == id).FirstOrDefault();
            
            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            foodItemRepo = new FoodItemRepo(db);
            foodItemRepo.Remove(id);
            return RedirectToAction("Index", "FoodItem");

        }



        [HttpGet]
        public IActionResult Create1()
        {
            // ViewData["Type"] = db.FoodType;

            IList<FoodType> foodTypeList = new List<FoodType>();
            foodTypeRepo = new FoodTypeRepo(db);
            foodTypeList = foodTypeRepo.GetAllType();
            //var 
            var newFoodType = foodTypeList.Select(f => new SelectListItem { Value = f.TypeName, Text = f.TypeName }).ToList();
            var typeList = new SelectList(newFoodType, "Value", "Text");
            ViewBag.TypeLists = typeList;
           // ViewData["foodTypeLists"] = new SelectList(db.FoodType, "TypeName");
            return View();
        }

        [HttpPost]
        public IActionResult Create1(FoodItemVM food, IFormFile imageUpload1)
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
                    //ViewData["TypeName"] = 

                    //ViewData["fileLocation"] = fileName;
                    image = "/" + Path.GetFileName(imageUpload1.FileName);
                    //newStay.Image = "/" + Path.GetFileName(imageUpload1.FileName);

                }
              //  result = foodItemRepo.CreateNew(food, image);
            }

            if (result == true)
            {
                return RedirectToAction("Index", "FoodItem");
            }
            else
            {
                return NotFound();
            }



            // return View();

        }
    }
}