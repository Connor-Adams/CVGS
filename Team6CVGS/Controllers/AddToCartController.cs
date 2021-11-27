using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Team6CVGS.Models;

namespace Team6CVGS.Controllers
{
    public class AddToCartController : Controller
    {
        public string ShoppingCartId { get; set; }
        private CVGSContext _context = new CVGSContext();
        public const string CartSessionKey = "CartId";

        public async Task<IActionResult> Index()
        {
            var cVGSContext = _context.ShoppingCartItems.Include(r => r.GameId).Include(r => r.DateCreated);
            return View(await cVGSContext.ToListAsync());
        }

        public IActionResult AddToCart(/*[Bind("ItemId,CartId,DateCreated,GameId")]*/ Guid game)
        {
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
            return Redirect("Games/Index");
        }

        public new void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        private readonly IHttpContextAccessor _contextAccessor;

        public string GetCartId(IHttpContextAccessor contextAccessor)
        {
            contextAccessor = _contextAccessor;

            if (contextAccessor.HttpContext.Session.Get(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(contextAccessor.HttpContext.User.Identity.Name))
                {
                    contextAccessor.HttpContext.Session.SetString(CartSessionKey, contextAccessor.HttpContext.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    contextAccessor.HttpContext.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return contextAccessor.HttpContext.Session.Get(CartSessionKey).ToString();
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId(_contextAccessor);

            return _context.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }
    }
}
