using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Repositories;

namespace Restaurant.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext _context;
        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            RoleRepo roleRepo = new RoleRepo(_context);
            return View();
        }
    }
}