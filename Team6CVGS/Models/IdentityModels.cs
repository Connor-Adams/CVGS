//using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Team6CVGS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {

        }

        public string DisplayName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CVGS")
        {
        }



        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<PROG3050_CVGS.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<PROG3050_CVGS.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}