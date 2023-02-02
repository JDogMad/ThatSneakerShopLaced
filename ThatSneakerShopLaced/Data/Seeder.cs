using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using ThatSneakerShopLaced.Areas.Identity.Data;
using ThatSneakerShopLaced.Models;

namespace ThatSneakerShopLaced.Data {
    public class Seeder {
        public static void Initialize(IApplicationBuilder builder1) {
            using (var service = builder1.ApplicationServices.CreateScope()) {
                var context = service.ServiceProvider.GetService<ApplicationDbContext>();
                var userManager = service.ServiceProvider.GetService<UserManager<Laced_User>>();
                context.Database.Migrate();
                context.Database.EnsureCreated();

                // No roles in the database 
                if (!context.Roles.Any()) {
                    context.Roles.AddRange(
                       new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" },
                       new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                       new IdentityRole { Id = "Manager", Name = "Manager", NormalizedName = "MANAGER" }
                    );
                    context.SaveChanges();

                    Laced_User UserJacqueline = new Laced_User {
                        Email = "jaquelinedoe123@gmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        UserName = "jaqDoe456",
                        FirstName = "Jaqueline",
                        LastName = "Doe",
                        Address = "6 Lees Creek Court Bronx, NY 10463"
                    };

                    Laced_User Admin = new Laced_User {
                        Email = "admin@laced23.be",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        UserName = "Admin",
                        FirstName = "-",
                        LastName = "-",
                        Address = "-",
                    };

                    Laced_User ManagerShopNY = new Laced_User {
                        Email = "help_NY@laced23.be",
                        EmailConfirmed = true,
                        LockoutEnabled = false,
                        UserName = "ManagerNY",
                        FirstName = "Manager NY",
                        LastName = "-",
                        Address = "250 West Yukon Dr. New York, NY 10002",
                        PostalCode = "10002"
                    };

                    var result1 = userManager.CreateAsync(UserJacqueline, "Abc123!").Result;
                    if (result1.Succeeded) {
                        // Add to user role
                        userManager.AddToRoleAsync(UserJacqueline, "User").Wait();
                    }
                    var result2 = userManager.CreateAsync(Admin, "Abc123!").Result;
                    if (result2.Succeeded) {
                        // Add to user role
                        userManager.AddToRoleAsync(Admin, "Admin").Wait();
                    }
                    var result3 = userManager.CreateAsync(ManagerShopNY, "Abc123!").Result;
                    if (result3.Succeeded) {
                        // Add to user role
                        userManager.AddToRoleAsync(ManagerShopNY, "Manager").Wait();
                    }
                } 
                
                if (!context.Category.Any()) {
                    context.Category.AddRange(
                        new Category { CategoryName = "Kids" },
                        new Category { CategoryName = "Women" },
                        new Category { CategoryName = "Men" });
                    context.SaveChanges();
                }

                if (!context.Shoe.Any()) {
                    context.Shoe.AddRange(
                        new Shoe { ShoeName = "Air Jordan 13 “Black Flint”", ShoeDescription = "Black/University Red-Flint Grey-White", ShoePrice = 200, Stock = 10, CategoryId = 3, ImageUrl = "https://i.ibb.co/QKLrVDD/j-Air-Black-Flint.webp" },
                        new Shoe { ShoeName = "Air Jordan 7 “Black Olive”", ShoeDescription = "Black/Cherrywood Red-Neutral Olive-Chutney", ShoePrice = 200, Stock = 9, CategoryId = 1, ImageUrl = "https://i.ibb.co/y5ZxkXS/j-Air-Black-Olive.webp" },
                        new Shoe { ShoeName = "Air Jordan 5 WMNS “Mars For Her”", ShoeDescription = "Martian Sunrise/Black-Fire Red-Muslin", ShoePrice = 200, Stock = 12, CategoryId = 2, ImageUrl = "https://i.ibb.co/zh9S9hx/j-Air-Mars-For-Her.webp" },
                        new Shoe { ShoeName = "Air Jordan 4 “Messy Room”", ShoeDescription = "Entitled “Messy Room,” this GS-exclusive Air Jordan 4 is officially inspired by kids’ lack of organization and the disheveled state in which their rooms are often found. Similar to the above-mentioned AJ1, this colorway employs a playful, split design, with light blues comprising the brunt of the construction’s forward half. ", ShoePrice = 140, Stock = 10, CategoryId = 1, ImageUrl = "https://i.ibb.co/wQTrdy5/j-Air-Messy-Room.webp" },
                        new Shoe { ShoeName = "Air Jordan 9 “Olive Concord”", ShoeDescription = "Black/Bright Concord-Light Olive-Aquatone", ShoePrice = 250, Stock = 2, CategoryId = 3, ImageUrl = "https://i.ibb.co/fGfg6xf/j-Air-Olive-Concord.webp" },
                        new Shoe { ShoeName = "Air Jordan 1 High OG “True Blue”", ShoeDescription = "True Blue/White-Cement Grey", ShoePrice = 180, Stock = 12, CategoryId = 2, ImageUrl = "https://i.ibb.co/SXRxms3/j-Air-True-Blue.webp" },
                        new Shoe { ShoeName = "Air Jordan 1 High OG “Washed Pink”", ShoeDescription = "Atmosphere/White-Muslin-Sail", ShoePrice = 180, Stock = 5, CategoryId = 2, ImageUrl = "https://i.ibb.co/q1Jy9Pg/j-Air-Washed-Pink.webp" },
                        new Shoe { ShoeName = "Air Jordan 1 Low OG “Year of the Rabbit”", ShoeDescription = "SAIL/UNIVERSITY RED-BROWN", ShoePrice = 653, Stock = 2, CategoryId = 3, ImageUrl = "https://i.ibb.co/FWWgwhT/j-Air-Year-Of-The-Rabbit.webp" },
                        new Shoe { ShoeName = "Air Jordan 1 Low “Panda”", ShoeDescription = "White/Black-White", ShoePrice = 150 , Stock = 7, CategoryId = 1, ImageUrl = "https://i.ibb.co/jD9X485/low-panda.webp" });
                    context.SaveChanges();
                }

                if (!context.Wishlist.Any()) {
                    context.Wishlist.AddRange(
                        new Wishlist { CustomerId = "109587cb-1938-4205-85ae-cf39feee8e87", ShoeId = 4 }, 
                        new Wishlist { CustomerId = "91dd77d2-1e26-4d41-a348-f6e526e97a55", ShoeId = 6 }
                        );
                    context.SaveChanges();
                }
            }
        }
    }
}
