using Bags_Wallets.Models;

namespace Bags_Wallets.Repository.Interface
{
    public interface IWishlistRepository
    {
        Task<Wishlist> GetByUserIdAsync(string userId);
        Task AddToWishlistAsync(string userId, int productId);
        Task RemoveFromWishlistAsync(string userId, int productId);
    }
}
