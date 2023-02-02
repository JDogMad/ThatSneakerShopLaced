using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace ThatSneakerShopLaced.Controllers {
    public class LanguageController : Controller {
        [HttpPost]
        public IActionResult SetLanguage(string culture) {
            Console.WriteLine("Setting culture to: " + culture);

            if (culture == null) { 
                return RedirectToAction("Index", "Cart");

            }

            Response.Cookies.Append(
              CookieRequestCultureProvider.DefaultCookieName,
              CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
              new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return RedirectToAction("Index", "Home");
        }
    }

}


