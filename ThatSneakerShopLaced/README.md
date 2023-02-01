
# Laced (ThatSneakerShop)

Laced is a website for rare sneakers. It's made by someone who loves shoes, and more so rare shoes. So I made a website to sell 
those shoes and share my love of sneakers with you. 


![Logo](https://i.postimg.cc/TPBjd1FM/Laced-Logo-removebg-preview-1.png)


## Documentation

First of all clone the ThatSneakerShopLaced_v1 branch.
When running it, you should be able to see the catalog of shoes.
To add shoes to your cart or wishlist, you will need to login or register. 

The website is available for 4 different roles, a visitor is someone that does not have an account and will be restricted in his or her action. Then you have the user, this user can add things to the cart, buy shoes, change personal information and add shoes to his or her wishlist. A Manager can do the things that a user also can, but has a extra page: the dashboard, there the manager can see what the shoe stock is, the categories and all the orders placed. Last type of role is the Admin, he has access to everything, he has access to user info, orders, payments, wishlists, carts, etc.. 

If you wish to test the payment out and place an order you will need this: 
   - testing succesfull payments:
        - Card: 4242424242424242 
        - Date: 12/2026 (anything in the future)
        - CVC: 999 (testing purposes)
   - testing failure payments:
        - Card: 4000000000009995 
        - Date: 12/2026 (anything in the future)
        - CVC: 999 (testing purposes)


### Default logins: 
- Admin - Abc123! (Role = Admin)
- ManagerNy - Abc123! (Role = Manager)
- jaqDoe456 - Abc123! (Role = User)



## Features

- Login/Register
- Wishlist
- Cart
- Payment with Stripe


## References
### Frontend references
- [Form](https://bbbootstrap.com/snippets/payment-form-three-different-payment-options-13285516)
- [Bootstrap forms](https://codepen.io/Kerrys7777/pen/QWgwEeG)
- [Order details](https://mdbootstrap.com/docs/standard/extended/order-details/)
- [View-grid](https://bbbootstrap.com/snippets/bootstrap-ecommerce-product-grid-view-card-19577966)
### Backend references
- [ASP.NET documentation](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio)
- [StackOverflow](https://stackoverflow.com/)
- [Stripe documentation](https://stripe.com/docs/api/customers/update?lang=dotnet)
- [Stripe Api Guide](https://blog.christian-schou.dk/implement-stripe-payments-in-asp-net6/)

## Authors

- [@Oumaima](https://github.com/JDogMad)

