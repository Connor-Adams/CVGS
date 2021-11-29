using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Team6CVGS.Data;
using Team6CVGS.Models;

namespace Team6CVGS.Controllers
{
    public class HomeController : Controller
    {
        private readonly CVGSContext _context;
        private readonly ILogger<HomeController> _logger;
        private RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleMgr, CVGSContext context)
        {
            roleManager = roleMgr;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int total = _context.Games.Count();
            Random r = new Random();
            int offset = r.Next(0, total);

            var cVGSContext = _context.Games.Skip(offset).FirstOrDefaultAsync();//.Include(g => g.EsrbRatingCodeNavigation).Include(g => g.GameCategory).Include(g => g.GamePerspectiveCodeNavigation).Include(g => g.GameStatusCodeNavigation).Include(g => g.GameSubCategory).Include(g => g.Guid);
            string trailer = "https://www.youtube.com/embed/mkHJzHIbQQg"; // Probably shouldn't be hardcoded, but they're all the same anyway and this was faster :)

            // Extract the youtube video id from the trailer column

            ViewData["trailer"] = trailer;

            return View(await cVGSContext);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
