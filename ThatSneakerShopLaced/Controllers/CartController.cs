using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThatSneakerShopLaced.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using ThatSneakerShopLaced.Models;
using ThatSneakerShopLaced.Sessions;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System;
using ThatSneakerShopLaced.Data;

namespace ThatSneakerShopLaced.Controllers {
    public class CartController : Controller {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context) {
            _context = context;

        }
        public IActionResult Index() {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel model = new()
            {
                CartItems = cart,
                TotalAmount = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(model);
        }

        public IActionResult Cart() {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartViewModel cartVM;
            if (cart == null || cart.Count == 0) {
                cartVM = null;
            } else {
                cartVM = new CartViewModel {
                    NumberOfItems = cart.Sum(x => x.Quantity),
                    TotalAmount = cart.Sum(x => x.Quantity * x.Price)
                };
            }
            return View("Cart", cartVM);
        }

        public async Task<IActionResult> Add(int id){
            Shoe shoe = await _context.Shoe.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.ShoeId == id).FirstOrDefault();
            if (cartItem == null){
                cart.Add(new CartItem(shoe));
            } else {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.setJson("Cart", cart);
            TempData["Succes"] = "The product has been added!";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult ShippingInfo() {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            var model = new UserViewModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                City = user.City,
                PostalCode = user.PostalCode,
                Country = user.Country
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SaveShippingInfo(UserViewModel model) {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.City = model.City;
            user.PostalCode = model.PostalCode;
            user.Country = model.Country;
            _context.SaveChanges();

            return RedirectToAction("NextStepInCheckoutProcess");
        }


    }
}
