using EMAProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMAProject.Classes
{
    /* Class handling:
     *   - Creation of roles
     *   - Assignment of roles to user
     *   - Removal of roles of user
     */
    public class RoleManagerHelper
    {
        private UserManager<IdentityUser> _userManager { get; set; }
        private RoleManager<IdentityRole> _roleManager { get; set; }

        public RoleManagerHelper(UserManager<IdentityUser> inUserManager, RoleManager<IdentityRole> inRoleManager) {
            _userManager = inUserManager;
            _roleManager = inRoleManager;
        }

        public IdentityResult CreateRoles(List<IdentityRole> roles) {
            return new IdentityResult();
        }
    }
}
