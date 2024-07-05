using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;
using System.Reflection.Emit;

namespace Bags_Wallets.Data
{
    public class ShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
   : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //base.OnModelCreating(builder);
            //builder.Entity<ShoppingCart>()
            //    .HasOne(c => c.ApplicationUser)
            //    .WithOne(u => u.ShoppingCart)
            //    .HasForeignKey<ShoppingCart>(c => c.ApplicationUserId);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PageImageCategory> PageImageCategories { get; set; }
        public DbSet<PageImage> PageImages { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }




    }
}

