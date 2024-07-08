using AutoMapper;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Bags_Wallets.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartItemRepository _shoppingCartrepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IShoppingCartItemRepository shoppingCartRepository, UserManager<ApplicationUser> userManager,
            IMapper mapper, IProductRepository productRepository)
        {
            _shoppingCartrepository = shoppingCartRepository;
            _userManager = userManager;
            _mapper = mapper;
            _productRepository = productRepository;


        }
       
        private async Task<string> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id;
        }

        public async Task<IActionResult> Index()
        {
            var userId = await GetCurrentUserId();
            var cartItems = await _shoppingCartrepository.GetItemsByUserIdAsync(userId);
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userId = await GetCurrentUserId();
            var cartItemViewModel = await _shoppingCartrepository.AddToCartAsync(userId, productId, quantity);
            return RedirectToAction("ShoppingCart", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _shoppingCartrepository.RemoveFromCartAsync(cartItemId);
            return RedirectToAction("Profile", "Account");
        }

    }
}