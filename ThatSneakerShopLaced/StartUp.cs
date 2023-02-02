﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Stripe;
using ThatSneakerShopLaced.Application;
using ThatSneakerShopLaced.Areas.Identity.Data;
using ThatSneakerShopLaced.Contracts;
using ThatSneakerShopLaced.Data;
namespace ThatSneakerShopLaced{
    public class StartUp {
        public StartUp(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;  
            });

            // Add Entity Framework and Identity
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<Laced_User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add Localization
            services.AddLocalization(option => option.ResourcesPath = "Localizing");

            // Add Api
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock Management API", Version = "v1" });
            });
            services.AddHttpClient();

            //services.AddTransient<IStripeAppService, StripeAppService>();
            services.AddStripeInfrastructure(Configuration);

            services.AddControllersWithViews();
            // Add MVC and Razor pages
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddRazorPagesOptions(options => {
                    options.Conventions.AuthorizePage("/Index");
                    options.Conventions.AuthorizePage("/Privacy");
                }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            services.AddLogging(logging => logging.AddConsole());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>{
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            app.UseMiddleware<LacedMiddleware>();


            // Middleware for the Swagger Api
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock Management API V1");
            });

            app.UseCors(options => options.WithOrigins("http://localhost:7002")
                              .AllowAnyMethod()
                              .AllowAnyHeader());
            Seeder.Initialize(app);
        }
    }
}
