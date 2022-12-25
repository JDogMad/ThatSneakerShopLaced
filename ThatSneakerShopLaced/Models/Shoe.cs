using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ThatSneakerShopLaced.Models {
    public class Shoe {
        [Key]
        public int ShoeId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Name")]
        public string ShoeName { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Description")]
        public string ShoeDescription { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        public decimal ShoePrice { get; set; }

        [Required]
        public int Stock { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Available")]
        public bool Hidden { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        // Foreign Key with Category //
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
