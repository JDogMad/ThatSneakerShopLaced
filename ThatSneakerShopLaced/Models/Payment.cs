using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ThatSneakerShopLaced.Models {
    public class Payment {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        [StringLength(128)]
        public string PaymentMethod { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Time of payment")]
        public DateTime TimeOfPayment { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Available")]
        public bool Hidden { get; set; }


        // Foreign Key with Order //
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
