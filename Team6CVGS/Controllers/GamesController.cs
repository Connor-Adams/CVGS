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
    public class GamesController : Controller
    {
        private readonly CVGSContext _context;

        public GamesController(CVGSContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            var cVGSContext = _context.Games;//.Include(g => g.EsrbRatingCodeNavigation).Include(g => g.GameCategory).Include(g => g.GamePerspectiveCodeNavigation).Include(g => g.GameStatusCodeNavigation).Include(g => g.GameSubCategory).Include(g => g.Guid);
            return View(await cVGSContext.ToListAsync());
        }

        // GET: specific game
        public async Task<IActionResult> Game(Guid guid, string trailer)
        {
            var cVGSContext = _context.Games.Where(g => g.Guid.Equals(guid));

            // Extract the youtube video id from the trailer column
            string videoid = trailer.Substring(91, 11);
            trailer = "https://www.youtube.com/embed/" + videoid;
            ViewData["trailer"] = trailer;

            //System.Diagnostics.Debug.WriteLine(videoid);

            return View(await cVGSContext.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.EsrbRatingCodeNavigation)
                .Include(g => g.GameCategory)
                .Include(g => g.GamePerspectiveCodeNavigation)
                .Include(g => g.GameStatusCodeNavigation)
                .Include(g => g.GameSubCategory)
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["EsrbRatingCode"] = new SelectList(_context.EsrbRatings, "Code", "Code");
            ViewData["GameCategoryId"] = new SelectList(_context.GameCategories, "Id", "EnglishCategory");
            ViewData["GamePerspectiveCode"] = new SelectList(_context.GamePerspectives, "Code", "Code");
            ViewData["GameStatusCode"] = new SelectList(_context.GameStatuses, "Code", "Code");
            ViewData["GameSubCategoryId"] = new SelectList(_context.GameSubCategories, "Id", "EnglishCategory");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,GameStatusCode,GameCategoryId,GameSubCategoryId,EsrbRatingCode,EnglishName,FrenchName,FrenchVersion,EnglishPlayerCount,FrenchPlayerCount,GamePerspectiveCode,EnglishTrailer,FrenchTrailer,EnglishDescription,FrenchDescription,EnglishDetail,FrenchDetail,UserName")] Game game)
        {
            if (ModelState.IsValid)
            {
                game.Guid = Guid.NewGuid();
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EsrbRatingCode"] = new SelectList(_context.EsrbRatings, "Code", "Code", game.EsrbRatingCode);
            ViewData["GameCategoryId"] = new SelectList(_context.GameCategories, "Id", "EnglishCategory", game.GameCategoryId);
            ViewData["GamePerspectiveCode"] = new SelectList(_context.GamePerspectives, "Code", "Code", game.GamePerspectiveCode);
            ViewData["GameStatusCode"] = new SelectList(_context.GameStatuses, "Code", "Code", game.GameStatusCode);
            ViewData["GameSubCategoryId"] = new SelectList(_context.GameSubCategories, "Id", "EnglishCategory", game.GameSubCategoryId);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["EsrbRatingCode"] = new SelectList(_context.EsrbRatings, "Code", "Code", game.EsrbRatingCode);
            ViewData["GameCategoryId"] = new SelectList(_context.GameCategories, "Id", "EnglishCategory", game.GameCategoryId);
            ViewData["GamePerspectiveCode"] = new SelectList(_context.GamePerspectives, "Code", "Code", game.GamePerspectiveCode);
            ViewData["GameStatusCode"] = new SelectList(_context.GameStatuses, "Code", "Code", game.GameStatusCode);
            ViewData["GameSubCategoryId"] = new SelectList(_context.GameSubCategories, "Id", "EnglishCategory", game.GameSubCategoryId);
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid,GameStatusCode,GameCategoryId,GameSubCategoryId,EsrbRatingCode,EnglishName,FrenchName,FrenchVersion,EnglishPlayerCount,FrenchPlayerCount,GamePerspectiveCode,EnglishTrailer,FrenchTrailer,EnglishDescription,FrenchDescription,EnglishDetail,FrenchDetail,UserName")] Game game)
        {
            if (id != game.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Guid))
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
            ViewData["EsrbRatingCode"] = new SelectList(_context.EsrbRatings, "Code", "Code", game.EsrbRatingCode);
            ViewData["GameCategoryId"] = new SelectList(_context.GameCategories, "Id", "EnglishCategory", game.GameCategoryId);
            ViewData["GamePerspectiveCode"] = new SelectList(_context.GamePerspectives, "Code", "Code", game.GamePerspectiveCode);
            ViewData["GameStatusCode"] = new SelectList(_context.GameStatuses, "Code", "Code", game.GameStatusCode);
            ViewData["GameSubCategoryId"] = new SelectList(_context.GameSubCategories, "Id", "EnglishCategory", game.GameSubCategoryId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.EsrbRatingCodeNavigation)
                .Include(g => g.GameCategory)
                .Include(g => g.GamePerspectiveCodeNavigation)
                .Include(g => g.GameStatusCodeNavigation)
                .Include(g => g.GameSubCategory)
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game = await _context.Games.FindAsync(id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(Guid id)
        {
            return _context.Games.Any(e => e.Guid == id);
        }
    }
}
