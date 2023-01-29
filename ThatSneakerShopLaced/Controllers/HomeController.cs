using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using ThatSneakerShopLaced.Areas.Identity.Data;
using ThatSneakerShopLaced.Data;
using ThatSneakerShopLaced.Models;
using ThatSneakerShopLaced.Models.ViewModels;

namespace ThatSneakerShopLaced.Controllers {
    public class HomeController : LacedController {

        public HomeController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<LacedController> logger)
            : base(context, httpContextAccessor, logger) {
        }

        public async Task<IActionResult> Index() {
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

        public IActionResult Dashboard(string userName, string firstName, string lastName, string email, int? pageNumber, string shoeName = null, string catName = null, int ordId = 0) {
            List<UserViewModel> vmUsers = new List<UserViewModel>();
            List<Laced_User> users = _context.Users.Where(u => u.UserName != "KingDima55"
                                                        && (u.UserName.Contains(userName) || string.IsNullOrEmpty(userName))
                                                        && (u.FirstName.Contains(firstName) || string.IsNullOrEmpty(firstName))
                                                        && (u.LastName.Contains(lastName) || string.IsNullOrEmpty(lastName))
                                                        && (u.Email.Contains(email) || string.IsNullOrEmpty(email))).ToList();
            foreach (Laced_User user in users) {
                vmUsers.Add(new UserViewModel {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Roles = (from userRole in _context.UserRoles
                             where userRole.UserId == user.Id
                             orderby userRole.RoleId
                             select userRole.RoleId).ToList()
                });
            }
            ViewData["userName"] = userName;
            ViewData["firstName"] = firstName;
            ViewData["lastName"] = lastName;
            ViewData["email"] = email;
            if (pageNumber == null) pageNumber = 1;

            List<ShoeViewModel> vmShoes = new List<ShoeViewModel>();
            var shoes = _context.Shoe.Where(s => s.ShoeName.Contains(shoeName) || string.IsNullOrEmpty(shoeName))
                                        .ToList();
            foreach (Shoe shoe in shoes){
                vmShoes.Add(new ShoeViewModel
                {
                    Name = shoe.ShoeName,
                    Stock = shoe.Stock,
                    Price = shoe.ShoePrice
                });
            }

            List<CategoryViewModel> cmCategories = new List<CategoryViewModel>();
            var categories = _context.Category.Where(c => c.CategoryName.Contains(catName) || string.IsNullOrEmpty(catName));
            foreach (Category category in categories) {
                cmCategories.Add(new CategoryViewModel{
                    Name = category.CategoryName,
                    Available = category.Hidden
                });
            }

            List<OrderViewModel> omOrders = new List<OrderViewModel>();
            var orders = _context.Order.ToList();
            foreach (Order order in orders) {
                omOrders.Add(new OrderViewModel {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    OrderTotal = order.Total,
                    Customer = order.Customer
                });
            }

            Paginas<UserViewModel> model = new Paginas<UserViewModel>(vmUsers, vmUsers.Count, 1, 10);
            ViewData["shoes"] = vmShoes;
            ViewData["categories"] = cmCategories;
            ViewData["orders"] = omOrders;

            return View(model);
        }
    }
}