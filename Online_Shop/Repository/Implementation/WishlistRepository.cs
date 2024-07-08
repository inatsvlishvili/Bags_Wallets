using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bags_Wallets.Repository.Implementation
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ShopDbContext _DbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public WishlistRepository(ShopDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _DbContext = dbContext;
            _userManager = userManager;

        }

        public async Task<Wishlist> GetByUserIdAsync(string userId)
        {
            return await _DbContext.Wishlists
                .Include(w => w.WishlistItems)
                .ThenInclude(wi => wi.Product)
                .ThenInclude(wp => wp.Images)
                .FirstOrDefaultAsync(w => w.ApplicationUserId == userId);
        }
        public async Task<Wishlist> GetAllWishlistAsync()
        {
            return await _DbContext.Wishlists
                .Include(y => y.ApplicationUser)
                .Include(w => w.WishlistItems)
                .ThenInclude(wi => wi.Product)
                .ThenInclude(wp => wp.Images)
                .FirstOrDefaultAsync();
        }

        public async Task AddToWishlistAsync(string userId, int productId)
        {
            var wishlist = await GetByUserIdAsync(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist { ApplicationUserId = userId };
                _DbContext.Wishlists.Add(wishlist);
                await _DbContext.SaveChangesAsync();
            }

            var wishlistItem = new WishlistItem
            {
                ProductId = productId,
                WishlistId = wishlist.Id
            };

            _DbContext.WishlistItems.Add(wishlistItem);
            await _DbContext.SaveChangesAsync();
        }

        public async Task RemoveFromWishlistAsync(string userId, int productId)
        {
            var wishlist = await GetByUserIdAsync(userId);
            if (wishlist != null)
            {
                var item = wishlist.WishlistItems.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    _DbContext.WishlistItems.Remove(item);
                    await _DbContext.SaveChangesAsync();
                }
            }
        }
    }
}
