using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThatSneakerShopLaced.Areas.Identity.Data;
using ThatSneakerShopLaced.Models;

namespace ThatSneakerShopLaced.Data {
    public class ApplicationDbContext : IdentityDbContext<Laced_User> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options){}

        public DbSet<ThatSneakerShopLaced.Models.Category> Category { get; set; }
        public DbSet<ThatSneakerShopLaced.Models.Order> Order { get; set; }
        public DbSet<ThatSneakerShopLaced.Models.Payment> Payment { get; set; }
        public DbSet<ThatSneakerShopLaced.Models.Shoe> Shoe { get; set; }
        public DbSet<ThatSneakerShopLaced.Models.Wishlist> Wishlist { get; set; }
    }
}