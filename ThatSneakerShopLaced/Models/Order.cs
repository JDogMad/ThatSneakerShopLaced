using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThatSneakerShopLaced.Areas.Identity.Data;

namespace ThatSneakerShopLaced.Models {
    public class Order {
        [Key]
        public int OrderId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Available")]
        public bool Hidden { get; set; }

        // Foreign Key with User //
        public string CustomerId { get; set; }

        [ForeignKey("Id")]
        public virtual Laced_User Customer { get; set; }
    }
}
