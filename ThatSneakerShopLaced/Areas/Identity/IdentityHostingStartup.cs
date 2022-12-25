using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThatSneakerShopLaced.Areas.Identity.Data;
using ThatSneakerShopLaced.Data;

[assembly: HostingStartup(typeof(ThatSneakerShopLaced.Areas.Identity.IdentityHostingStartup))]
namespace ThatSneakerShopLaced.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
