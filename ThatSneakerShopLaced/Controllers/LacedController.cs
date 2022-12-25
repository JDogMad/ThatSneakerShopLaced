using Microsoft.AspNetCore.Mvc;
using ThatSneakerShopLaced.Areas.Identity.Data;
using ThatSneakerShopLaced.Data;

namespace ThatSneakerShopLaced.Controllers{
    public class LacedController : Controller {
        protected readonly ApplicationDbContext _context;
        protected readonly ILogger<LacedController> _logger;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly Laced_User _user;

        public LacedController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<LacedController> logger) {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

            _user = _context.Users.FirstOrDefault(u => u.UserName == httpContextAccessor.HttpContext.User.Identity.Name);
        }

    }
}
