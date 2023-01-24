using Stripe;

namespace ThatSneakerShopLaced.Models.Stripe {
    public record AddStripeCustomer(
        string Email,
        string Name,
        AddStripeCard CreditCard);
}
