using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMAProject.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMAProject.Controllers
{
    public class RoleManagerController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        
        public RoleManagerController(UserManager<IdentityUser> inUserManager, RoleManager<IdentityRole> inRoleManager) 
        {
            _userManager = inUserManager;
            _roleManager = inRoleManager;
        }

        //D.F. Show all users in the system and their assigned roles.
        public IActionResult Index()
        {
            return View();
        }
    }
}
