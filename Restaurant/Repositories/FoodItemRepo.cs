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
            var category = db.FoodCategory.Where(fc => fc.CategoryId == Convert.ToInt32(food.ItemCategory)).FirstOrDefault();
            FoodItem NewFoodItem = new FoodItem
            {
                    Name =  food.Name,
                    Image = image,
                    Quantity = 0,
                    UnitPrice =food.UnitPrice,
                    ItemCategory = category.CategoryName,
                    FoodTypeId = food.FoodTypeId
                   // Type = food.Type
            };

            var typeName = db.FoodType.Where(ft => ft.FoodTypeId == food.FoodTypeId).FirstOrDefault();
            FoodType NewType = new FoodType
            {
                TypeName = typeName.TypeName,
                CategoryId = Convert.ToInt32(food.ItemCategory)
            };
            db.FoodType.Add(NewType);
            db.FoodItem.Add(NewFoodItem);
            db.SaveChanges();

           

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
            var category = db.FoodCategory.Where(fc => fc.CategoryId == Convert.ToInt32(food.ItemCategory)).FirstOrDefault();
            FoodItem foodItem = new FoodItem
            {
                Name = food.Name,
                Image = food.Image,
                UnitPrice = food.UnitPrice,
                ItemCategory = category.CategoryName

            };
            db.FoodItem.Update(foodItem);
            db.SaveChanges();
            return true;
        }
        public bool Remove(int id)
        {
            var entry = db.FoodItem.Where(f => f.FoodId == id).FirstOrDefault();
            if(entry != null)
            {
                db.FoodItem.Remove(entry);
                db.SaveChanges();
            }           

            //db.FoodItem.Remove(id);
            return true;
        }
    }
}
