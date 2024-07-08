using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Bags_Wallets.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;


namespace Bags_Wallets.Repository.Implementation
{
    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly ShopDbContext _DbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShoppingCartItemRepository(ShopDbContext dbContext, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _DbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShoppingCart> GetItemsByUserIdAsync(string userId)
        {
            var useritem = await _DbContext.ShoppingCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(x => x.Images)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

            return useritem;
        }
        public async Task<ShoppingCart> GetAllItemsAsync()
        {
            var useritem = await _DbContext.ShoppingCarts
                .Include(y => y.ApplicationUser)
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(x => x.Images)
                .FirstOrDefaultAsync();

            return useritem;
        }

        public async Task<ShoppingCartItemViewModel> AddToCartAsync(string userId, int productId, int quantity)
        {
            var cart = await _DbContext.ShoppingCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    ApplicationUserId = userId,
                    Items = new List<ShoppingCartItem>()
                };
                _DbContext.ShoppingCarts.Add(cart);
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem == null)
            {
                var product = await _DbContext.Products.FindAsync(productId);
                if (product == null)
                {
                    throw new ApplicationException("Product not found.");
                }

                cartItem = new ShoppingCartItem
                {
                    Price = product.Price,
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product,
                    ShoppingCart = cart
                };
                cart.Items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _DbContext.SaveChangesAsync();
            var viewModel = new ShoppingCartItemViewModel
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.Product.Title,
                ProductPrice = cartItem.Product.Price,
                Quantity = cartItem.Quantity
            };

            return viewModel;
        }
        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var cartItem = await _DbContext.ShoppingCartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _DbContext.ShoppingCartItems.Remove(cartItem);
                await _DbContext.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await _DbContext.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

            if (cart != null)
            {
                _DbContext.ShoppingCartItems.RemoveRange(cart.Items);
                await _DbContext.SaveChangesAsync();
            }
        }

    }
}