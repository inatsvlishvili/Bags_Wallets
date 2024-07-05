//using Microsoft.AspNetCore.Mvc;
//using Bags_Wallets.Models;
//using Microsoft.AspNetCore.Identity;
//using Bags_Wallets.Data;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
//using Bags_Wallets.ViewModels;
//using Microsoft.EntityFrameworkCore;
//using Bags_Wallets.Repository.Interface;
//using AutoMapper;


//namespace Bags_Wallets.Controllers
//{
//    public class AccountController : BaseController
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly IShoppingCartItemRepository _shoppingCartrepository;
//        private readonly IMapper _mapper;

//        //private readonly ShopDbContext DbContext;


//        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ShopDbContext DbContext,
//            IShoppingCartItemRepository shoppingCartRepository, IMapper mapper) : base(DbContext)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _shoppingCartrepository = shoppingCartRepository;
//            _mapper = mapper;
//            //this.DbContext = dbContext;

//        }
//        private async Task<string> GetCurrentUserId()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            return user?.Id;
//        }
//        public async Task<IActionResult> ShoppingCart()
//        {
//            var userId = await GetCurrentUserId();
//            ViewBag.image = await _DbContext.Products.Include(x => x.Images).Where(x => x.Id == x.Id).FirstOrDefaultAsync();
//            var cartItems = await _shoppingCartrepository.GetItemsByUserIdAsync(userId);
//            var viewModel = _mapper.Map<ShoppingCartViewModel>(cartItems);

//            return View(viewModel);
//        }



//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel vm)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new ApplicationUser { UserName = vm.Email, Email = vm.Email, PhoneNumber = vm.PhoneNumber, FirstName = vm.FirstName, LastName = vm.LastName };
//                //var user = new ApplicationUser { UserName = (vm.FirstName) + "." + (vm.LastName), Email = vm.Email, PhoneNumber = vm.PhoneNumber };
//                user.City = vm.City;
//                var result = await _userManager.CreateAsync(user, vm.Password);

//                if (result.Succeeded)
//                {
//                    await _userManager.AddToRoleAsync(user, "User");


//                    await _signInManager.SignInAsync(user, isPersistent: false);
//                    return RedirectToAction("Index", "Home");
//                }
//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError("", error.Description);
//                }
//            }

//            return RedirectToAction("Index", "Home");

//        }
//        [HttpPost]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            return RedirectToAction("Index", "Home");
//        }
//        //[HttpGet]
//        //public IActionResult Login()
//        //{
//        //    return View();
//        //}


//        [HttpGet]
//        public IActionResult Login(string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;

//            if (ModelState.IsValid)
//            {


//                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);

//                if (result.Succeeded)
//                {
//                    return RedirectToLocal(returnUrl);
//                }
//                else
//                {
//                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//                    return View(vm);
//                }
//            }

//            // If we got this far, something failed, redisplay form
//            return View(vm);
//        }

//        private IActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction(nameof(HomeController.Index), "Home");
//            }
//        }


//        [HttpGet]

//        public async Task<IActionResult> Profile(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);

//            return View();
//        }
//        [HttpPost]

//        public IActionResult Profile()
//        {
//            //var itemdetails = await userManager.Users.Where(x => x.Id == id).ToListAsync();


//            return View();
//        }

//        [HttpGet]
//        public IActionResult ChangePassword()
//        {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> ChangePassword(ChangePassowrdViewModel vm)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(vm);
//            }

//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            }

//            var changePasswordResult = await _userManager.ChangePasswordAsync(user, vm.OldPassword, vm.NewPassword);
//            if (!changePasswordResult.Succeeded)
//            {
//                foreach (var error in changePasswordResult.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//                return View(vm);
//            }

//            await _signInManager.RefreshSignInAsync(user);
//            return RedirectToAction(nameof(AccountController.Profile), "Account");
//        }

//        public async Task<IActionResult> UpdateProfile(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);

//            if (user == null)
//            {
//                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
//                return View("NotFound");
//            }

//            var model = new EditUserViewModel
//            {
//                Id = user.Id,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                Email = user.Email,
//                PhoneNumber = user.PhoneNumber,
//                City = user.City,
//                Address = user.Address,
//            };


//            return View(model);
//        }


//        [HttpPost]
//        public async Task<IActionResult> UpdateProfile(EditUserViewModel vm)
//        {
//            var user = await _userManager.FindByIdAsync(vm.Id);
//            //var user = await userManager.GetUserAsync(User);


//            if (!ModelState.IsValid)
//            {
//                return View(vm);
//            }


//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            }

//            user.FirstName = vm.FirstName;
//            user.LastName = vm.LastName;
//            user.Email = vm.Email;
//            user.PhoneNumber = vm.PhoneNumber;
//            user.City = vm.City;
//            user.Address = vm.Address;
//            //user.ImageName = vm.ImageName;

//            var UpdateProfileResult = await _userManager.UpdateAsync(user);

//            if (!UpdateProfileResult.Succeeded)
//            {
//                foreach (var error in UpdateProfileResult.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//                return View(vm);
//            }


//            await _signInManager.RefreshSignInAsync(user);
//            return RedirectToAction(nameof(AccountController.Profile), "Account");
//        }

//    }

//}
