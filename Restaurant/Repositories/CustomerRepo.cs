﻿using Restaurant.Models;
using Restaurant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Repositories
{
    public class CustomerRepo
    {
        private RestaurantContext db;
        private IServiceProvider _serviceProvider;
        private readonly RestaurantContext _context;
        public CustomerRepo(RestaurantContext db, IServiceProvider serviceProvider)
        {
            this.db = db;
            _serviceProvider = serviceProvider;
        }
        
        // Add new customers
        public bool addNewCustomer(CustomerVM cust,string userId)
        {
            //UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider, _context);
            //var addUR = userRoleRepo.AddUserRole(userId,
            //                                                "Member");
            Customer customer = new Customer
            {
                LastName = cust.LastName,
                FirstName = cust.FirstName,
                Email = cust.Email,
                Phone = cust.Phone,
                Address = cust.Address,
                Street = cust.Street,
                City = cust.City,
                ProfileStatus = true,
                Userid = userId
            };
            db.Customer.Add(customer);
            db.SaveChanges();
            return true;
        }

        // Get a customer
        public Customer GetCustomer(int id)
        {
             var customer = db.Customer.Where(c => c.CustomerId == id).FirstOrDefault();

             return customer;
        }

        //Get all customers
        public IEnumerable<Customer> GetAllCustomers()
        {
            
            return db.Customer;

        }


      }
}
