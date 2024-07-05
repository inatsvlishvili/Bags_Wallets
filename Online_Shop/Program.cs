using Microsoft.EntityFrameworkCore;
using Bags_Wallets.Data;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Extensions.DependencyInjection;
using Bags_Wallets.Models;
using Microsoft.Extensions.Options;
using System.Configuration;
using Bags_Wallets.Repository.Implementation;
using Bags_Wallets.Repository.Interface;
using AutoMapper;
using Bags_Wallets.Mapping;

namespace Bags_Wallets
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");





            builder.Services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connectionString));



            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {




                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                //options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 30;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;

            }).AddEntityFrameworkStores<ShopDbContext>();



            //builder.Services.AddControllersWithViews();
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //       options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddIdentityCore<IdentityUser>()
            //        .AddEntityFrameworkStores<ApplicationDbContext>()
            //        .AddSignInManager()
            //        .AddDefaultTokenProviders();

            //builder.Services.AddAuthentication(o =>
            //    {
            //        o.DefaultScheme = IdentityConstants.ApplicationScheme;
            //        o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            //    })
            //    .AddIdentityCookies(o => { });

            /*
              services.Configure<IdentityOptions>(options =>
  {
      // Password settings.
      options.Password.RequireDigit = false;
      options.Password.RequireLowercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequireUppercase = false;
      options.Password.RequiredLength = 1;
      //options.Password.RequiredUniqueChars = 1;

      // Lockout settings.
      options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
      options.Lockout.MaxFailedAccessAttempts = 30;
      options.Lockout.AllowedForNewUsers = true;

      // User settings.
      options.User.AllowedUserNameCharacters =
      "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
      options.User.RequireUniqueEmail = true;
  });
             */



            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ShopDbContext>();
            //builder.Services.AddScoped<IEmailSender, EmailSender>();
            //builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ShopDbContext>();


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();//view ro ganaxldes updates mere
            builder.Services.AddControllersWithViews(); // damatebuli
            builder.Services.AddRazorPages(); // damatebuli
            builder.Services.AddScoped<ISellerRepository, SellerRepository>();
            builder.Services.AddScoped<IContactRepository, ContacRepository>();
            builder.Services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPageImageRepository, PageImageRepository>();
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            //builder.Services.AddScoped<IOrderRepository, OrderRepository>();


            //builder.Services.AddScoped<IItemRepository, ItemRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Manager", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));


                }

            }

            app.Run();
        }
    }
}
