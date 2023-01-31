using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using ThatSneakerShopLaced.Areas.Identity.Data;
using ThatSneakerShopLaced.Data;
using ThatSneakerShopLaced.Models;

namespace ThatSneakerShopLaced.Controllers {

    [Authorize(Roles = "Manager, Admin")]
    public class OrdersController : Controller {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index() {
              return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Order == null) {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null) {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,Total,Hidden,CustomerId")] Order order) {
            if (ModelState.IsValid) {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Order == null) {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null) {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,Total,Hidden,CustomerId")] Order order) {
            if (id != order.OrderId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!OrderExists(order.OrderId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            var orders = _context.Order.Find(id);
            if (orders == null) {
                // Category not found
                return NotFound();
            }

            // Ik gebruik gewoon mijn property bool Hidden om deze te verstoppen 
            // In de cshtml doe ik een if else structuur om deze te verstoppen of te tonen 
            orders.Hidden = true;
            _context.SaveChanges();

            // Redirect the user back to the view
            return RedirectToAction(nameof(Index));
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Order == null) {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null) {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id) {
          return _context.Order.Any(e => e.OrderId == id);
        }
    }   
}
