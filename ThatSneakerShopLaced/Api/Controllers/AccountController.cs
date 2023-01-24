using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ThatSneakerShopLaced.Areas.Identity.Data;
using ThatSneakerShopLaced.Data;
using ThatSneakerShopLaced.Api.Models;

namespace ThatSneakerShopLaced.Api.Controllers {

    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase{
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<Laced_User> _signInManager;

        public AccountController(ApplicationDbContext context, SignInManager<Laced_User> signInManager) {
            _context = context;
            _signInManager = signInManager;
        }

        // POST: api/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Boolean>> Login([FromBody] LoginModel @login) {
            var result = await _signInManager.PasswordSignInAsync(@login.UserName, @login.Password, false, lockoutOnFailure: false);
            if (result.Succeeded) {
                return true;
            }
            return false;
        }

    }
}
