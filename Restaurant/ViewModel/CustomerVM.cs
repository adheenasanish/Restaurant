using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class CustomerVM
    {
        public int customer_Id { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        [DisplayName("Profile Status")]
        public bool? ProfileStatus { get; set; }
    }
}
