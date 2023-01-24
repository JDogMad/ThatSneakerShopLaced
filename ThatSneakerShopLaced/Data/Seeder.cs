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

                    context.UserRoles.AddRange(
                           new IdentityUserRole<string> { RoleId = "User", UserId = Admin.Id },
                           new IdentityUserRole<string> { RoleId = "Admin", UserId = Admin.Id },
                           new IdentityUserRole<string> { RoleId = "Manager", UserId = Admin.Id }
                       );
                    context.SaveChanges();

                    // I had a error, it seemed that the roles were being made but the users not 
                    // Then I made a new database and it worked perfectly 
                    // But just in case roles are made and users not 
                    // I create them here 
                } else {
                    Laced_User UserDima = new Laced_User {
                        Email = "king-dima@gmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = true,
                        UserName = "KingDima420",
                        FirstName = "Dima",
                        LastName = "King",
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

                    // Seed users 
                    var result = userManager.CreateAsync(UserDima, "Abc123!").Result;
                    if (result.Succeeded) {
                        // Add to user role
                        userManager.AddToRoleAsync(UserDima, "User").Wait();
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

                //TODO: Dont forget to change pictures because they are ugly
                if (!context.Shoe.Any()) {
                    context.Shoe.AddRange(
                        new Shoe { ShoeName = "Triple Pink Dunks", ShoeDescription = "The Nike Dunk Low Triple Pink has a full leather build, which makes use of three distinct shades of pink to complete the look. The base has a light shade of pink, with a slightly contrasting pink overlay.", ShoePrice = 221, Stock = 5, CategoryId = 1, ImageUrl = "https://i.ibb.co/GVjTSBQ/Nike-Dunk-Low-Triple-Pink.jpg" },
                        new Shoe { ShoeName = "Nike SB Dunk Low Pro", ShoeDescription = "GREEN/METALLIC GOLD-WHITE-LIGHT GUM", ShoePrice = 147, Stock = 1, CategoryId = 2, ImageUrl = "https://i.ibb.co/yQ60bB9/Nike-Sb-Dunk-Low-Pro.jpg" },
                        new Shoe { ShoeName = "Nike Dunk Low Halloween (2022)", ShoeDescription = "PHANTOM/BLACK-SAFETY ORANGE", ShoePrice = 210, Stock = 5, CategoryId = 3, ImageUrl = "https://i.ibb.co/3rzKzct/Nike-Dunk-Low-Halloween.jpg" },
                        new Shoe { ShoeName = "Chocolate x Nike SB Dunk Low", ShoeDescription = "This Dunk Low was created in collaboration with West Coast label Chocolate Skateboards, sister company to Girl Skateboards, and was released as the companion to the Zoo York Dunk. The cross detailing on the heel served to commemorate the life of legendary Chocolate team rider Keenan Milton, who passed away in 2002.", ShoePrice = 1875, Stock = 1, CategoryId = 3, ImageUrl = "https://i.ibb.co/23mzjRm/most-expensive-nike-sb-dunks-09.webp" },
                        new Shoe { ShoeName = "Parra x Nike SB Dunk Low (Friends & Family)", ShoeDescription = "Parra and Nike are no strangers to collaboration, having teamed up on a number of Air Max 1s in the past. In 2019, the duo linked up again, this time working on both a Blazer and Dunk low in all-white, with typical hits of Parra color. The Friends & Family version of the Dunk was never released to the public, thus making it so collectible.", ShoePrice = 3054, Stock = 2, CategoryId = 3, ImageUrl = "https://i.ibb.co/mRjTd4k/most-expensive-dunks-parra-ff-01.webp" },
                        new Shoe { ShoeName = "Nike SB Dunk Low “Paris”", ShoeDescription = "The most expensive sneaker on this list, the Paris Dunk Low was part of the same pack as Jeff Staple’s NYC Pigeon Dunk above. Rumor has it 202 pairs were produced, each featuring original artwork by Bernard Buffet on the panels along the laces, heel, and toe box.", ShoePrice = 19500, Stock = 1, CategoryId = 3, ImageUrl = "https://i.ibb.co/47pH26B/most-expensive-nike-sb-dunks-17.webp" });
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
