using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThatSneakerShopLaced.Data;
using ThatSneakerShopLaced.Models;

namespace ThatSneakerShopLaced.Controllers {
    public class HomeController : LacedController {

        public HomeController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<LacedController> logger)
            : base(context, httpContextAccessor, logger) {
        }

        public async Task<IActionResult> Index()
        {
            var shoes = await _context.Shoe.ToListAsync();
            return View(shoes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}