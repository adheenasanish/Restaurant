using System;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public partial class Administrator
    {
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }
        public int? MenuId { get; set; }

        public Menu Menu { get; set; }
    }
}
