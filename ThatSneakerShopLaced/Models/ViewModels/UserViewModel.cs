using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ThatSneakerShopLaced.Models.ViewModels {
    public class UserViewModel {
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }
        public string Email { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public List<String> Roles { get; set; }
    }

    public class RoleViewModel {
        public string UserName { get; set; }

        public List<string> Roles { get; set; }
    }
}
