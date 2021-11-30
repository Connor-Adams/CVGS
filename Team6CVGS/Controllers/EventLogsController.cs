using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team6CVGS.Models;

namespace Team6CVGS.Controllers
{
    public class EventLogsController : Controller
    {
        private readonly CVGSContext _context;

        public EventLogsController(CVGSContext context)
        {
            _context = context;
        }

        // GET: EventLogs
        public async Task<IActionResult> Index()
        {
            var CVGSContext = _context.EventLogs
                .Include(p => p.Date)
                .Include(p => p.Event)
                .Include(p => p.Detail)
                .OrderBy(x => x.Date)
                .ThenBy(y => y.Event);
            return View(await _context.EventLogs.ToListAsync());
        }

        // GET: EventLogs/Details/5
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

        // GET: EventLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,Date,Category,Event,Detail")] EventLog eventLog)
        {
            if (ModelState.IsValid)
            {
                eventLog.Guid = Guid.NewGuid();
                _context.Add(eventLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventLog);
        }

        // GET: EventLogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventLog = await _context.EventLogs.FindAsync(id);
            if (eventLog == null)
            {
                return NotFound();
            }
            return View(eventLog);
        }

        // POST: EventLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid,Date,Category,Event,Detail")] EventLog eventLog)
        {
            if (id != eventLog.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventLogExists(eventLog.Guid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eventLog);
        }

        // GET: EventLogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

        // POST: EventLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var eventLog = await _context.EventLogs.FindAsync(id);
            _context.EventLogs.Remove(eventLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventLogExists(Guid id)
        {
            return _context.EventLogs.Any(e => e.Guid == id);
        }
    }
}
