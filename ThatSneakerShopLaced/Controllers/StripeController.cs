using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Security.Claims;
using ThatSneakerShopLaced.Contracts;
using ThatSneakerShopLaced.Data;
using ThatSneakerShopLaced.Models;
using ThatSneakerShopLaced.Models.Stripe;
using ThatSneakerShopLaced.Models.ViewModels;

namespace ThatSneakerShopLaced.Controllers {
    [Authorize(Roles = "User, Manager, Admin")]
    [Route("api/[controller]")]
    public class StripeController : Controller {
        private readonly IStripeAppService _stripeService;
        private readonly ApplicationDbContext _context;

        public StripeController(IStripeAppService stripeService, ApplicationDbContext context) {
            _stripeService = stripeService;
            _context = context;
        }


        [HttpGet("customer/add")]
        public IActionResult AddCustomer() {
            decimal total = Convert.ToDecimal(TempData.Peek("Total"));
            HttpContext.Session.SetInt32("Total", Convert.ToInt32(total));

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Find(userId);
            var model = new UserViewModel {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            var email = user.Email;
            var name = model.FirstName + " " + model.LastName;
            var customer = new AddStripeCustomer(email, name, null);
            return View(customer);
        }

        [HttpPost("customer/add")]
        public async Task<ActionResult<StripeCustomer>> AddStripeCustomer(IFormCollection formData, CancellationToken ct) {
            var customer = new AddStripeCustomer(
                formData["email"],
                formData["fullName"],
                new AddStripeCard(
                    formData["fullName"],
                    formData["CreditCard.CardNumber"],
                    formData["CreditCard.ExpirationYear"],
                    formData["CreditCard.ExpirationMonth"],
                    formData["CreditCard.Cvc"])
            );

            try {
                StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(customer, ct);

                int total = HttpContext.Session.GetInt32("Total").Value;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.Find(userId);

                Order order = new Order {
                    OrderDate = DateTime.Now,
                    Total = total,
                    CustomerId = user.Id,
                    Customer = user,
                };
                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetInt32("OrderId", order.OrderId);

                return RedirectToAction("AddPayment", createdCustomer);
            } catch (StripeException ex) {
                ViewData["ErrorMessage"] = ex.Message;
                int totals = HttpContext.Session.GetInt32("Total").Value;
                ViewData["Total"] = totals;
                return View("AddCustomer", customer);
            }
        }

        [HttpGet("payment/add")]
        public IActionResult AddPayment() {
            int uuid = GenerateUniqueCode();
            DateTime shipping = DateTime.Now.AddDays(7);

            ViewData["Uuid"] = uuid;
            ViewData["Shipping"] = shipping;
            return View();
        }


        [HttpPost("payment/add")]
        public async Task<ActionResult<StripePayment>> AddStripePayment(CancellationToken ct) {
            int total = HttpContext.Session.GetInt32("Total").Value;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Find(userId);
            var email = user.Email;

            var customerService = new CustomerService();
            var customer = customerService.List(new CustomerListOptions { Email = email }).FirstOrDefault();

            var orderId = HttpContext.Session.GetInt32("OrderId").GetValueOrDefault();

            if (customer != null) {
                string customerId = (string)customer.Id;
                string customerEmail = customer.Email;
                string description = "Thank you for shopping at Laced";
                string currency = "EUR";
                long amount = (long)total * 100;

                var customerPayment = new AddStripePayment(
                    customerId,
                    customerEmail,
                    description,
                    currency,
                    amount
                );

                try {
                    StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(customerPayment, ct);

                    Payment pay = new Payment {
                        PaymentMethod = "VISA",
                        Amount = amount / 100,
                        TimeOfPayment = DateTime.Now,
                        OrderId = orderId
                    };
                    _context.Payment.Add(pay);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                } catch (StripeException ex) {
                    ViewData["ErrorMessage"] = ex.Message;
                    return View("AddPayment");
                }
            }

            return null;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public int GenerateUniqueCode() {
            int orderNumber;
            do {
                orderNumber = new Random().Next(100000, 999999);
            } while (_context.Order.Any(o => o.OrderId == orderNumber));

            return orderNumber;
        }

    }
}
