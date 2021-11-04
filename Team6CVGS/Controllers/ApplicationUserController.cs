using System.Data.Entity;
using System.Threading.Tasks;
using PROG3050_CVGS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Team6CVGS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public ApplicationUsersController()
        {

        }


        [System.Web.Mvc.HttpGet]
        public ViewResult Index()
        {
            var context = new ApplicationDbContext();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roles = new List<string>();

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMngr = new RoleManager<IdentityRole>(roleStore);

            roles = roleMngr.Roles.Select(x => x.Name).ToList();
            ViewBag.Roles = new SelectList(roles);

            // From https://www.c-sharpcorner.com/article/list-of-users-with-roles-in-mvc-asp-net-identity/
            var lum = new ListUserViewModel();
            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new ListUserViewModel()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      Role = string.Join(",", p.RoleNames)
                                  });

            return View(usersWithRoles);
            // ----------


        }

        public RedirectResult ChangeRole(string userId, string oldRole, string roleName)
        {
            var context = new ApplicationDbContext();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            Debug.WriteLine(manager.FindById(userId).Claims);
            Debug.WriteLine("USERID: " + userId);
            Debug.WriteLine("RoleName:" + roleName);
            if (roleName != "")
            {

                if (oldRole != null)
                {
                    manager.RemoveFromRole(userId, oldRole);

                }

                manager.AddToRole(userId, roleName);
            }

            context.SaveChanges();
            manager.UpdateSecurityStamp(userId);

            return Redirect("Index");
        }
    }
}
