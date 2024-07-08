using Microsoft.AspNetCore.Mvc;
using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;
using Bags_Wallets.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace Bags_Wallets.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartItemRepository _shoppingCartrepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, UserManager<ApplicationUser> userManager, IMapper mapper, IShoppingCartItemRepository shoppingCartRepository)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _mapper = mapper;
            _shoppingCartrepository = shoppingCartRepository;

        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel viewModel)
        {
            var userId = _userManager.GetUserId(User);
            var cart = await _shoppingCartrepository.GetItemsByUserIdAsync(userId);
            if (cart == null || !cart.Items.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            var order = new Order
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                City = viewModel.City,
                Address = viewModel.Address,
                MobileNumber = viewModel.MobileNumber,
                Email = viewModel.Email,
                PaymentMethod = viewModel.PaymentMethod,
                Description = viewModel.Description,
                ApplicationUserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = cart.Items.Sum(item => item.Product.Price * item.Quantity)
            };

            foreach (var cartItem in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price

                };
                order.OrderItems.Add(orderItem);
            }

            await _orderRepository.AddAsync(order);
            await _shoppingCartrepository.ClearCartAsync(userId);

            return RedirectToAction("Profile", "Account");
        }
    }
}