using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class CustomerVM
    {
        public int customer_Id { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [DisplayName("Last Name")]        
        public string LastName { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [DisplayName("Phone")]
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [DisplayName("Profile Status")]
        public bool? ProfileStatus { get; set; }
    }
}
