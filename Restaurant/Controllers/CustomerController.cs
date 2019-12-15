﻿using System;
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
    public class CustomerController : Controller
    {
        private RestaurantContext db;
        private CustomerRepo custRepo;
        private IServiceProvider _serviceProvider;

        public CustomerController(RestaurantContext db, IServiceProvider _serviceProvide)
        {
            this.db = db;
            this._serviceProvider = _serviceProvider;
            custRepo = new CustomerRepo(db, _serviceProvider);
        }
        public IActionResult Index()
        {
            IEnumerable<Customer> customerList = custRepo.GetAllCustomers(); 
            return View(customerList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(CustomerVM cVm)
        {
            string userName = HttpContext.User.Identity.Name;

            var ss = HttpContext.Session.GetInt32("SessionKeyName");

            var userData = db.AspNetUsers.Where(a => a.UserName == userName).FirstOrDefault();
            var id = userData.Id;

            bool result = false;
            if (ModelState.IsValid)
            {
                result = custRepo.addNewCustomer(cVm,userName,id);

            }  
            
            if( result == true)
            {
                return RedirectToAction("Create", "Order");
            }
            else
            {
                return NotFound();
            }  
       
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            //var result;

            //if (id == 0)
            //{
              var  result = custRepo.GetCustomer(Convert.ToInt32(id));
            //}
            //else
            //{
                return View(result);

            //}


        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var result = custRepo.GetCustomer(Convert.ToInt32(id));

            return View(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return View();
        }

       

    }
}