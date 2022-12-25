using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThatSneakerShopLaced.Areas.Identity.Data;

namespace ThatSneakerShopLaced.Models {
    public class Wishlist {
        [Key]
        public int WishlistId { get; set; }

        // Foreign Key with User //
        public string CustomerId { get; set; }

        [ForeignKey("Id")]
        public virtual Laced_User Customer { get; set; }

        [ForeignKey("ShoeId")]
        public Shoe Shoe { get; set; }
        public int ShoeId { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Available")]
        public bool Hidden { get; set; }
    }
}
