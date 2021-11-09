using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team6CVGS.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Team6CVGS.Models;

namespace Team6CVGS.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;

        public ApplicationUsersController(RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> userMgr, ApplicationDbContext _context)
        {
            roleManager = roleMgr;
            userManager = userMgr;
            context = _context;
        }

        public ViewResult Index()
        {
            var Users = new List<IdentityUser>();
            List<IdentityUser> UserList;
            Users = context.Users.ToList();

            var roles = new List<string>();
            roles = roleManager.Roles.Select(x => x.Name).ToList();
            ViewBag.Roles = new SelectList(roles);

            return View(Users);
            // ----------


        }

        public ViewResult ChangeRoleAsync(string roleName)
        {

            ViewBag.RoleName = roleName;
            var Users = userManager.GetUsersInRoleAsync(roleName).Result.ToList();

            var roles = roleManager.Roles.Select(x => x.Name).ToList();
            ViewBag.Roles = new SelectList(roles);


            return View(Users);
        }

        public RedirectResult RoleChange(string userId, string roleName, string oldRole)
        {
            var user = userManager.FindByIdAsync(userId).Result;

            if (roleName != "")
            {
                if (oldRole != null)
                {
                    userManager.RemoveFromRoleAsync(user, oldRole).Wait();
                }

                userManager.AddToRoleAsync(user, roleName).Wait();
            }

            context.SaveChanges();
            userManager.UpdateSecurityStampAsync(user).Wait();

            return Redirect("Index");
        }
    }
}
