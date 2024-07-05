using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Bags_Wallets.Data;
using Bags_Wallets.Repository.Interface;
using AutoMapper;
using Bags_Wallets.Repository.Implementation;

namespace Bags_Wallets.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ShopDbContext _DbContext;
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IShoppingCartItemRepository _shoppingCartrepository;
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;


        public AdminController(ILogger<AdminController> logger, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, ShopDbContext dbContext, IMapper mapper, IWishlistRepository wishlistRepository,
            IShoppingCartItemRepository shoppingCartrepository, IOrderRepository orderRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _DbContext = dbContext;
            _mapper = mapper;
            _wishlistRepository = wishlistRepository;
            _shoppingCartrepository = shoppingCartrepository;  
            _orderRepository = orderRepository;

        }

        public async Task<IActionResult> Sold()
        {
            var userId = _userManager.GetUserId(User);
            var orders = await _orderRepository.GetAllAsync();
            var userOrders = orders.Where(o => o.ApplicationUserId == userId);
            var orderViewModels = _mapper.Map<IEnumerable<OrderViewModel>>(userOrders);
            return View(orderViewModels);
        }
        private async Task<string> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id;
        }
        public async Task<IActionResult> CartAdded()
        {
            var userId = await GetCurrentUserId();
            var cartItems = await _shoppingCartrepository.GetItemsByUserIdAsync(userId);
            var cartViewModel = _mapper.Map<ShoppingCart>(cartItems);
            return View(cartViewModel);
        }
        public async Task<IActionResult> Favorite()
        {
            var userId = _userManager.GetUserId(User);
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            var wishlistViewModel = _mapper.Map<Wishlist>(wishlist);

            return View(wishlistViewModel);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users()
        {
            var users = _userManager.Users;
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            // GetClaimsAsync retunrs the list of user Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,

                //UserName = user.UserName,
                City = user.City,
                //Claims = userClaims.Select(c => c.Value).ToList(),
                // Roles = userRoles
            };

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                var role = roles[0];
                model.Role = role;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.City = model.City;
                user.Address = model.Address;
                user.ImageName = model.ImageName;

                var result = await _userManager.UpdateAsync(user);


                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
                await _userManager.AddToRoleAsync(user, model.Role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }


        public IActionResult DeleteUser()
        {

            return View();
        }

        public IActionResult ContactInfo()
        {

            return View();
        }

    }
}
