using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThatSneakerShopLaced.Contracts;
using ThatSneakerShopLaced.Data;
using ThatSneakerShopLaced.Models.Stripe;
using ThatSneakerShopLaced.Models.ViewModels;

namespace ThatSneakerShopLaced.Controllers {
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
            var customer = new AddStripeCustomer (
                formData["email"],
                formData["fullName"],
                new AddStripeCard(
                    formData["fullName"], 
                    formData["CreditCard.CardNumber"],
                    formData["CreditCard.ExpirationYear"],
                    formData["CreditCard.ExpirationMonth"], 
                    formData["CreditCard.Cvc"])
            );


            StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(customer, ct);

            return StatusCode(StatusCodes.Status200OK, createdCustomer);
        }


        [HttpPost("payment/add")]
        public async Task<ActionResult<StripePayment>> AddStripePayment([FromBody] AddStripePayment payment, CancellationToken ct) {
            StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(
                payment,
                ct);

            return StatusCode(StatusCodes.Status200OK, createdPayment);
        }
    }
}
