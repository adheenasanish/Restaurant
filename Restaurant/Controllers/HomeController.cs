using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;

        // Constructor receives the context through dependency injection.
        public HomeController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // Home page shows list of items. Item price is set through ViewBag.
        public IActionResult Index()
        {
            ViewBag.TotalPrice = "1.31";
            //ViewBag.UnitPrice = "2.30";
            //ViewBag.Quantity = 1;    
            ViewBag.userId = "adheena@gmail.com";
            var items = _context.IPNs;
            return View(items);
        }
        //[HttpGet]
        //public IActionResult create()
        //{
        //    return View(_context);

        //}

        // This method receives and stores the Paypal transaction details.
        [HttpPost]
        public JsonResult PaySuccess([FromBody]IPN ipn)
        {
            try
            {
                _context.IPNs.Add(ipn);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return Json(ipn);
        }

        // Show transaction detail. 
        public IActionResult FinishShopping(string paymentID)
        {
            IPN transaction = _context.IPNs.Where(t => t.paymentID == paymentID).FirstOrDefault();
            return View(transaction);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
