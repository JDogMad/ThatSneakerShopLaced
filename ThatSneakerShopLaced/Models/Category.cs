using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThatSneakerShopLaced.Models {
    public class Category {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }


        [DefaultValue(false)]
        [Display(Name = "Available")]
        public bool Hidden { get; set; }
    }
}
