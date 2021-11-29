using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team6CVGS.Models;

namespace Team6CVGS.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly CVGSContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private  IHttpContextAccessor _contextAccessor;

        public ShoppingCartController(RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> userMgr, CVGSContext context, IHttpContextAccessor httpContextAccessor)
        {
            roleManager = roleMgr;
            userManager = userMgr;
            _context = context;
            _contextAccessor = httpContextAccessor;
        }

        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public async Task<IActionResult> AddToCart(/*[Bind("ItemId,CartId,DateCreated,GameId")]*/ Guid game)
        {
            ShoppingCartId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var cartItem = _context.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.GameId == game);
            if (cartItem == null)
            {
                // Create new cart item if no cart item exists
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    GameId = game,
                    CartId = ShoppingCartId,
                    Game = _context.Games.SingleOrDefault(
                    p => p.Guid == game),
                    DateCreated = DateTime.Now
                };

                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // do nothing.

            }
            _context.SaveChanges();
            return Redirect("Index");
        }

        public new void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }  

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Get all the cart items
            var test = _context.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();

            // For every item, readd the game property based on the GameId (because for some reason it doesn't save them...?)
            foreach (var game in test)
            {
                game.Game = _context.Games.SingleOrDefault(g => g.Guid == game.GameId);
            }

            return test;
        }

        // GET: ShoppingCart
        public IActionResult Index()
        {
            return View(GetCartItems().ToList());
        }

        // GET: ShoppingCart/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.ShoppingCartItems
                .Include(c => c.Game)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: ShoppingCart/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "Guid", "EnglishName");
            return View();
        }

        // POST: ShoppingCart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,CartId,DateCreated,GameId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Guid", "EnglishName", cartItem.GameId);
            return View(cartItem);
        }

        // GET: ShoppingCart/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.ShoppingCartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "Guid", "EnglishName", cartItem.GameId);
            return View(cartItem);
        }

        // POST: ShoppingCart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemId,CartId,DateCreated,GameId")] CartItem cartItem)
        {
            if (id != cartItem.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.ItemId))
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
            ViewData["GameId"] = new SelectList(_context.Games, "Guid", "EnglishName", cartItem.GameId);
            return View(cartItem);
        }

        // GET: ShoppingCart/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.ShoppingCartItems.FindAsync(id);
            _context.ShoppingCartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            /*
            var cartItem = await _context.ShoppingCartItems
                .Include(c => c.Game)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }
            */
            return View("Index");
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cartItem = await _context.ShoppingCartItems.FindAsync(id);
            _context.ShoppingCartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(string id)
        {
            return _context.ShoppingCartItems.Any(e => e.ItemId == id);
        }
    }
}
