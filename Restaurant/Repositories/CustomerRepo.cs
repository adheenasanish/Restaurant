using Restaurant.Models;
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
        public bool addNewCustomer(CustomerVM cust,string email, string userId)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider, _context);
            var addUR = userRoleRepo.AddUserRole(userId,
                                                            "Member");
            Customer customer = new Customer
            {
                LastName = cust.LastName,
                FirstName = cust.FirstName,
                Email = email,
                Phone = cust.Phone,
                Address = cust.Address,
                Street = cust.Street,
                City = cust.City,
                ProfileStatus = true,
                UserId = userId
               
            };
            db.Customer.Add(customer);
            db.SaveChanges();
            return true;
        }

        // Get a customer
        public CustomerVM GetCustomer(int id)
        {
             var customer = db.Customer.Where(c => c.CustomerId == id).FirstOrDefault();

            CustomerVM customerVM = new CustomerVM
            {
                LastName = customer.LastName,
                FirstName = customer.FirstName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Street = customer.Street,
                City = customer.City,
                ProfileStatus = customer.ProfileStatus
            };


             return customerVM;
        }

        //Get all customers
        public IEnumerable<Customer> GetAllCustomers()
        {
            
            return db.Customer;

        }


      }
}
