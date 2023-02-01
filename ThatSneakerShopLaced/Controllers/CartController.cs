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
using Microsoft.AspNetCore.Authorization;

namespace ThatSneakerShopLaced.Controllers {
    public class CartController : Controller {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context) {
            _context = context;

        }

        public IActionResult Index() {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel model = new() {
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

        // Create a new cart or increase the quantity by 1
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

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // Decrease Quantity by 1
        public async Task<IActionResult> Decrease(int id) {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ShoeId == id).FirstOrDefault();

            if(cartItem.Quantity > 1) {
                --cartItem.Quantity;
            } else {
                cart.RemoveAll(c => c.ShoeId == id);
            }

            if (cart.Count == 0) {
                HttpContext.Session.Remove("Cart");
            } else {
                HttpContext.Session.setJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        // Remove item from cart
        public async Task<IActionResult> Remove(int id) {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            var itemToRemove = cart.FirstOrDefault(c => c.ShoeId == id);
            if (itemToRemove != null) {
                cart.Remove(itemToRemove);
            }
            HttpContext.Session.setJson("Cart", cart);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User, Manager, Admin")]
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

        [Authorize(Roles = "User, Manager, Admin")]
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

            return RedirectToAction("AddCustomer", "Stripe");
        }
    }
}
