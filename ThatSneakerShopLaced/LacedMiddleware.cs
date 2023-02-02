using Microsoft.AspNetCore.Authentication;

namespace ThatSneakerShopLaced {
    public class LacedMiddleware {
        RequestDelegate _next;

        public LacedMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            if (!context.User.Identity.IsAuthenticated) {
                await context.ChallengeAsync("Identity.Application");
                return;
            }

            await _next(context);
        }
    }
}
