using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Repositories
{
    public class FoodItemRepo
    {
        private RestaurantContext db;
        private readonly IHostingEnvironment hostingEnvironment;

        public FoodItemRepo(RestaurantContext db)
        {
            this.db = db;
          
        }

        //Get all Food items
        public IEnumerable<FoodItem> GetAllFoodItems()
        {

            return db.FoodItem;

        }

        //Create new Item
        public bool CreateNew(FoodItem food, string image)
        {
            //if (imageUpload1 != null)
            //{
            //    var fileName = Path.Combine(hostingEnvironment.WebRootPath, Path.GetFileName(imageUpload1.FileName));
            //    imageUpload1.CopyTo(new FileStream(fileName, FileMode.Create));

            //    //ViewData["fileLocation"] = fileName;
            //    food.Image = "/" + Path.GetFileName(imageUpload1.FileName);
            //    //newStay.Image = "/" + Path.GetFileName(imageUpload1.FileName);

            //}

            FoodItem NewFoodItem = new FoodItem
            {
                    Name =  food.Name,
                    Image = image,
                    Quantity = food.Quantity,
                    UnitPrice =food.UnitPrice,
                    ItemCategory = food.ItemCategory,
                    Type = food.Type
            };
            db.FoodItem.Add(NewFoodItem);
            db.SaveChanges();

            //Menu menu = new Menu
            //{
            //    Price = food.UnitPrice,


            //}

            //db.FoodItem.Add(NewFoodItem);
           return true;
        }

        public FoodItem GetDetails(int id)
        {
            var details = db.FoodItem.Where(f => f.FoodId == id).FirstOrDefault();
            return details;
        }

        public bool Update(FoodItem food)
        {
            db.FoodItem.Update(food);
            db.SaveChanges();
            return true;
        }
        //public bool remove(int id)
        //{
        //    //db.FoodItem.Remove
        //}
    }
}
