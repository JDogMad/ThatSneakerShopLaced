using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThatSneakerShopLaced.Areas.Identity.Data {
    public class Laced_User : IdentityUser {
        [Required]
        [MaxLength(100)]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Last name")]
        public string LastName { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }
    }
}
