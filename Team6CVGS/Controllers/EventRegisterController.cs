using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team6CVGS.Models;

namespace Team6CVGS.Controllers
{
    public class EventRegisterController : Controller
    {
        private readonly CVGSContext _context;

        public EventRegisterController(CVGSContext context)
        {
            _context = context;
        }

        // GET: EventRegister
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventLogs.ToListAsync());
        }

        // GET: EventRegister/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLog = await _context.EventLogs
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (eventLog == null)
            {
                return NotFound();
            }

            return View(eventLog);
        }

        private bool EventLogExists(Guid id)
        {
            return _context.EventLogs.Any(e => e.Guid == id);
        }
    }
}
