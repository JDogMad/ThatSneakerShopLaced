using System.ComponentModel.DataAnnotations;

namespace ThatSneakerShopLaced.Models {
    public class CartItem {
        [Key]
        public int CartItemId { get; set; }
        public int ShoeId { get; set; }
        public string ShoeName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public decimal Total { 
            get{ 
                return Quantity * Price; 
            }
        }


        public CartItem(){}
        public CartItem(Shoe shoe) {
            ShoeId = shoe.ShoeId;
            ShoeName = shoe.ShoeName;
            Price = shoe.ShoePrice;
            Quantity = 1;
            Image = shoe.ImageUrl;
            Stock = shoe.Stock;
        }

    }
}
