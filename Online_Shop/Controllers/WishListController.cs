using AutoMapper;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Bags_Wallets.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bags_Wallets.Controllers
{
    public class WishListController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IWishlistRepository _wishlistRepository;
        private readonly UserManager<ApplicationUser> _userManager;



        public WishListController(IMapper mapper, IWishlistRepository wishlistRepository, UserManager<ApplicationUser> userManager)
        {

            _mapper = mapper;
            _wishlistRepository = wishlistRepository;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            var wishlistViewModel = _mapper.Map<WishlistViewModel>(wishlist);
            return View(wishlistViewModel);
        }

        public async Task<IActionResult> AddToWishlist(int productId)
        {
            ViewBag.proId = productId;
            var userId = _userManager.GetUserId(User);
            await _wishlistRepository.AddToWishlistAsync(userId, productId);
            return RedirectToAction("Profile", "Account");
        }

        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            var userId = _userManager.GetUserId(User);
            await _wishlistRepository.RemoveFromWishlistAsync(userId, productId);
            return RedirectToAction("Index");
        }
    }
}
