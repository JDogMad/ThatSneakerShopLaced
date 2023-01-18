namespace ThatSneakerShopLaced.Models.ViewModels {
    public class CartViewModel {
        public List<CartItem> CartItems { get; set; }
        public int NumberOfItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
