using Microsoft.AspNetCore.Mvc;
using Bags_Wallets.Models;
using Microsoft.AspNetCore.Identity;
using Bags_Wallets.Data;
using Bags_Wallets.ViewModels;
using Microsoft.EntityFrameworkCore;
using Bags_Wallets.Repository.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Bags_Wallets.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IShoppingCartItemRepository _shoppingCartrepository;
        private readonly IMapper _mapper;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ShopDbContext DbContext,
            IShoppingCartItemRepository shoppingCartRepository, IMapper mapper, IUserRepository userRepository) : base(DbContext)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _shoppingCartrepository = shoppingCartRepository;
            _mapper = mapper;

        }
        private async Task<string> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id;
        }
        public async Task<IActionResult> ShoppingCart()
        {
            var userId = await GetCurrentUserId();
            var cartItems = await _shoppingCartrepository.GetItemsByUserIdAsync(userId);
            var viewModel = _mapper.Map<ShoppingCartViewModel>(cartItems);


            return View(viewModel);
        }

        [HttpGet]

        public async Task<IActionResult> Profile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Profile()
        {
            var itemdetails = await _userManager.Users.Where(x => x.Id == x.Id).ToListAsync();

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(vm);
                }
            }

            return View(vm);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userRepository.AddAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditUserViewModel>(user);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                //user.DateOfBirth = model.DateOfBirth;
                user.Email = model.Email;

                await _userRepository.UpdateAsync(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<EditUserViewModel>(user);
            return View(model);
        }

        [HttpPost, ActionName("DeleteProfile")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProfileConfirmed()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userRepository.DeleteAsync(userId);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}