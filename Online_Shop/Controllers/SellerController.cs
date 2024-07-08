using Microsoft.AspNetCore.Mvc;
using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;
using Bags_Wallets.Repository.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Bags_Wallets.Controllers
{
    public class SellerController : Controller
    {
        private readonly ShopDbContext _DbContext;
        private readonly ISellerRepository _sellerRepository;
        private readonly IMapper _mapper;

        public SellerController(ShopDbContext dbContext, ISellerRepository sellerRepository, IMapper mapper)
        {
            _DbContext = dbContext;
            _sellerRepository = sellerRepository;
            _mapper = mapper;

        }

        public async Task<IActionResult> Seller()
        {
            var sellers = await _sellerRepository.GetAllSellersAsync();
            var sellerViewModels = _mapper.Map<IEnumerable<SellerViewModel>>(sellers);
            return View(sellerViewModels);
        }


        public IActionResult AddSeller()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddSeller(SellerViewModel sellerViewModel)
        {
            if (ModelState.IsValid)
            {
                sellerViewModel.CreatedateTime = DateTime.Now;
                var seller = _mapper.Map<Seller>(sellerViewModel);
                await _sellerRepository.AddSellerAsync(seller);
                return RedirectToAction(nameof(Seller));
            }
            return View(sellerViewModel);
        }


        public async Task<IActionResult> EditSeller(int id)
        {
            var seller = await _sellerRepository.GetSellerByIdAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            var sellerViewModel = _mapper.Map<SellerViewModel>(seller);
            return View(sellerViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditSeller(int id, SellerViewModel sellerViewModel)
        {
            if (id != sellerViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                sellerViewModel.CreatedateTime = DateTime.Now;
                var seller = _mapper.Map<Seller>(sellerViewModel);
                await _sellerRepository.UpdateSellerAsync(seller);
                return RedirectToAction(nameof(Seller));
            }
            return View(sellerViewModel);
        }

        public async Task<IActionResult> DeleteSeller(int id)
        {
            var seller = await _sellerRepository.GetSellerByIdAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            var ViewModel = _mapper.Map<SellerViewModel>(seller);
            return View(ViewModel);
        }

        [HttpPost]

        public async Task<IActionResult> DeleteSeller(SellerViewModel viewModel)
        {
            await _sellerRepository.DeleteSellerAsync(viewModel.Id);
            return RedirectToAction(nameof(Seller));
        }
        public async Task<IActionResult> SearhSeller(string query)
        {

            if (string.IsNullOrEmpty(query))
            {
                return PartialView("_SearchResults", await _DbContext.Sellers
                .Select(p => new SellerViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    FullName = p.FullName,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    City = p.City,
                    Address = p.Address,
                    Description = p.Description,
                    CreatedateTime = p.CreatedateTime,
                })
                .ToListAsync());
            }

            var results = await _DbContext.Sellers
                .Where(p => p.Title.Contains(query))
                .Select(p => new SellerViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    FullName = p.FullName,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    City = p.City,
                    Address = p.Address,
                    Description = p.Description,
                    CreatedateTime = p.CreatedateTime,
                })
                .ToListAsync();

            return PartialView("_SearchResults", results);
        }
    }
}