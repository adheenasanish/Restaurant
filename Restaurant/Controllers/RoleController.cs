using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Repositories;
using Restaurant.ViewModel;

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
           // IEnumerable < RoleRepo > = roleRepo.GetAllRoles();
            return View(roleRepo.GetAllRoles());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                RoleRepo roleRepo = new RoleRepo(_context);
                var success = roleRepo.CreateRole(roleVM.RoleName);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Error = "An error occurred while creating this role. Please try again.";
            return View();
        }

    }
}