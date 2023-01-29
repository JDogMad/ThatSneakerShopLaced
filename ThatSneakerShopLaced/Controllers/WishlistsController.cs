using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThatSneakerShopLaced.Data;
using ThatSneakerShopLaced.Models;
using ThatSneakerShopLaced.Sessions;

namespace ThatSneakerShopLaced.Controllers
{
    public class WishlistsController : Controller {
        private readonly ApplicationDbContext _context;

        public WishlistsController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Wishlists
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.Wishlist.Include(s => s.Shoe).Include(s => s.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Wishlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wishlist == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist
                .Include(s => s.Shoe)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.WishlistId == id);
            if (wishlist == null)
            {
                return NotFound();
            }

            return View(wishlist);
        }

        // GET: Wishlists/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Users, "UserId", "UserName");
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "ShoeId", "ShoeName");
            return View();
        }

        // POST: Wishlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WishlistId,CustomerId,ShoeId,Hidden")] Wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Users, "UserId", "UserName", wishlist.CustomerId);
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "ShoeId", "ShoeName", wishlist.ShoeId);
            return View(wishlist);
        }

        // GET: Wishlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wishlist == null)
            {
                return NotFound();
            }

            var wishlist = await _context.Wishlist.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Users, "UserId", "UserName", wishlist.CustomerId);
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "ShoeId", "ShoeName", wishlist.ShoeId);
            return View(wishlist);
        }

        // POST: Wishlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WishlistId,ShoeId,CustomerId,Hidden")] Wishlist wishlist)
        {
            if (id != wishlist.WishlistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishlistExists(wishlist.WishlistId))
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
            ViewData["Id"] = new SelectList(_context.Users, "UserId", "UserName", wishlist.CustomerId);
            ViewData["ShoeId"] = new SelectList(_context.Shoe, "ShoeId", "ShoeName", wishlist.ShoeId);
            return View(wishlist);
        }

        // GET: Wishlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var lists = _context.Wishlist.Find(id);
            if (lists == null)
            {
                // Category not found
                return NotFound();
            }

            // Ik gebruik gewoon mijn property bool Hidden om deze te verstoppen 
            // In de cshtml doe ik een if else structuur om deze te verstoppen of te tonen 
            lists.Hidden = true;
            _context.SaveChanges();

            // Redirect the user back to the view
            return RedirectToAction(nameof(Index));
        }

        // POST: Wishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Wishlist == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Wishlist'  is null.");
            }
            var wishlist = await _context.Wishlist.FindAsync(id);
            if (wishlist != null)
            {
                _context.Wishlist.Remove(wishlist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(int id)
        {
          return _context.Wishlist.Any(e => e.WishlistId == id);
        }

        public async Task<IActionResult> Add(int id) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shoe = await _context.Shoe.FirstOrDefaultAsync(s => s.ShoeId == id);
            if (shoe == null) {
                return NotFound();
            }

            var existingWishlist = await _context.Wishlist.FirstOrDefaultAsync(w => w.ShoeId == id && w.CustomerId == userId);
            if (existingWishlist != null) {
                return RedirectToAction("Index", "Home");
            }

            var wishlist = new Wishlist {
                CustomerId = userId,
                ShoeId = shoe.ShoeId,
                Shoe = await _context.Shoe.FindAsync(id),
                Customer = await _context.Users.FindAsync(userId)
            };
            _context.Wishlist.Add(wishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> GetMyWishlist() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = await _context.Wishlist
                .Include(w => w.Shoe)
                .Where(w => w.CustomerId == userId)
                .ToListAsync();
            return View(wishlist);
        }
        public async Task<IActionResult> Remove(int id) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingWishlist = await _context.Wishlist.FirstOrDefaultAsync(w => w.ShoeId == id && w.CustomerId == userId);
            if (existingWishlist == null) {
                return NotFound();
            }

            _context.Wishlist.Remove(existingWishlist);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetMyWishlist");
        }



    }
}
