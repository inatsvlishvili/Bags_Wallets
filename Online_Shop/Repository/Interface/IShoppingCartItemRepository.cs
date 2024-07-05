using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;

namespace Bags_Wallets.Repository.Interface
{
    public interface IShoppingCartItemRepository
    {
        //ShoppingCart GetCartByUserId(string userId);
        Task<ShoppingCart> GetItemsByUserIdAsync(string userId);
        Task<ShoppingCartItemViewModel> AddToCartAsync(string userId, int productId, int quantity);
        Task RemoveFromCartAsync(int cartItemId);
        Task ClearCartAsync(string userId);
        //Task<OrderViewModel> DoCheckoutAsync(string userId);
     
    }
}

