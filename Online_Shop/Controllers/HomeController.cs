using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;
using Bags_Wallets.Repository.Interface;
using AutoMapper;
using System.Drawing.Printing;
using Bags_Wallets.Repository.Implementation;

namespace Bags_Wallets.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public HomeController(IProductRepository productRepository, IMapper mapper,
            IContactRepository contactRepository, ShopDbContext DbContext) : base(DbContext)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _contactRepository = contactRepository;

        }
        public async Task<IActionResult> Index()
        {
            ViewBag.CoverPhoto = await _DbContext.PageImages.Where(x => x.PageImageCategory.Title == "MainCover").OrderBy(r => Guid.NewGuid()).ToListAsync();
            ViewBag.NewAdd = await _DbContext.Products.Include(x => x.Images).OrderByDescending(x => x.CreatedateTime).Take(20).ToListAsync();
            ViewBag.Sale = await _DbContext.Products.Include(x => x.Images).Where(x=>x.IsOnSale == true).Take(20).OrderBy(r => Guid.NewGuid()).ToListAsync();

            //var products = await _productRepository.GetAllProductsAsync();
            //var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View();

        }

        public async Task<IActionResult> Bags(int page = 1)
        {
            //if (!string.IsNullOrEmpty(type))
            //{
            //    ViewBag.TypeParam = "type=" + type;
            //}
            ViewBag.ProductInfo = await _DbContext.Products.Where(x => x.Category.Title == "Bag").Where(x => x.Id == x.Id).FirstOrDefaultAsync();
            ViewBag.BagCoverPhoto = await _DbContext.PageImages.Where(x => x.PageImageCategory.Title == "BagCover").OrderBy(r => Guid.NewGuid()).ToListAsync();
            ViewBag.thumb = await _DbContext.Products.Include(x => x.Images).Where(x => x.Id == x.Id).FirstOrDefaultAsync();

            const int pageSize = 10; // Set the number of products per page
            var (products, totalCount) = await _productRepository.GetAllBagAsync(page, pageSize);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var productViewModels = _mapper.Map<List<ProductViewModel>>(products);

            var viewModel = new ProductListViewModel
            {
                Products = productViewModels,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);


        }
        public async Task<IActionResult> Wallets(int page = 1)
        {
            ViewBag.ProductInfo = await _DbContext.Products.Where(x => x.Category.Title == "Wallet").Where(x => x.Id == x.Id).FirstOrDefaultAsync();
            ViewBag.WalletCoverPhoto = await _DbContext.PageImages.Where(x => x.PageImageCategory.Title == "WalletCover").OrderBy(r => Guid.NewGuid()).ToListAsync();

            const int pageSize = 10; // Set the number of products per page
            var (products, totalCount) = await _productRepository.GetAllWalletAsync(page, pageSize);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var productViewModels = _mapper.Map<List<ProductViewModel>>(products);

            var viewModel = new ProductListViewModel
            {
                Products = productViewModels,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);


            //var products = await _productRepository.GetAllWalletAsync();
            //var viewModel = _mapper.Map<IEnumerable<ProductViewModel>>(products);

            //return View(viewModel);
        }
        public async Task<IActionResult> FilterWallets(ProductListViewModel filter)
        {
            var products = await _productRepository.GetFilteredBagssAsync(filter);
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return PartialView("_FilterResult", productViewModels);

        }
        public IActionResult ClearWalletsFilters()
        {
            var allProducts = _productRepository.GetWalletAsync();
            var viewModelProducts = _mapper.Map<List<ProductViewModel>>(allProducts);
            return View(viewModelProducts);
        }
        public async Task<IActionResult> FilterBags(ProductListViewModel filter)
        {
            var products = await _productRepository.GetFilteredBagssAsync(filter);
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return PartialView("_FilterResult", productViewModels);

        }
        public IActionResult ClearBagsFilters()
        {
            var allProducts = _productRepository.GetBagAsync();
            var viewModelProducts = _mapper.Map<List<ProductViewModel>>(allProducts);
            return View(viewModelProducts);
        }
        public async Task<IActionResult> ProductFilter(ProductListViewModel filter)
        {
            var products = await _productRepository.GetFilteredSaleProductAsync(filter);
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return PartialView("_SaleFilterResult", productViewModels);
        }

        public IActionResult ClearProductsFilters()
        {
            var allProducts = _productRepository.GetAllProductsAsync();
            var viewModelProducts = _mapper.Map<List<ProductViewModel>>(allProducts);
            return View(viewModelProducts);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var products = await _productRepository.GetByIdAsync(id);


            if (!ModelState.IsValid)
            {
                return View();
            }

            var viewModel = _mapper.Map<ProductViewModel>(products);

            return View(viewModel);


        }
        [HttpPost]
        public async Task<IActionResult> ProductDetail(CommentViewModel viewModel, ProductViewModel productmdel)
        {
            if (ModelState.IsValid)
            {
                viewModel.CreatedateTime = DateTime.Now;
                productmdel.Id = viewModel.Id;
                ViewBag.ProductId = productmdel.Id;
                var comment = _mapper.Map<Comment>(viewModel);
                await _contactRepository.AddCommentAsync(comment);
                return RedirectToAction(nameof(ProductDetail));
            }
            return View(viewModel);

        }

        public async Task<IActionResult> AboutUs()
        {

            var aboutUs = await _DbContext.AboutUs.FirstOrDefaultAsync();

            return View(aboutUs);
        }
        public async Task<IActionResult> Contact()
        {
            ViewBag.ContactInfo = await _DbContext.ContactInfos.FirstOrDefaultAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(ContactUsViewModel viewModel)
        {
            ViewBag.ContactInfo = await _DbContext.ContactInfos.FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                var contactUs = _mapper.Map<ContactUs>(viewModel);
                await _contactRepository.AddContactUsAsync(contactUs);
                return RedirectToAction(nameof(Contact));
            }

            return View();
        }

        public async Task<IActionResult> SearchBag(string query)
        {

            if (string.IsNullOrEmpty(query))
            {
                return PartialView("_FilterResult", await _DbContext.Products.Where(x => x.Category.Title == "Bag")
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Price = p.Price,
                    Title = p.Title,
                    Collection = p.Collection,
                    Images = p.Images
                })
                .ToListAsync());
            }

            var results = await _DbContext.Products.Where(x => x.Category.Title == "Bag")
                .Where(p => p.Title.Contains(query))
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Collection = p.Collection,
                    Images = p.Images
                })
                .ToListAsync();

            return PartialView("_FilterResult", results);
        }

        public async Task<IActionResult> SearchWallet(string query)
        {

            if (string.IsNullOrEmpty(query))
            {
                return PartialView("_FilterResult", await _DbContext.Products.Where(x => x.Category.Title == "Wallet")
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Price = p.Price,
                    Title = p.Title,
                    Collection = p.Collection,
                    Images = p.Images
                })
                .ToListAsync());
            }

            var results = await _DbContext.Products.Where(x => x.Category.Title == "Wallet")
                .Where(p => p.Title.Contains(query))
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Collection = p.Collection,
                    Images = p.Images
                })
                .ToListAsync();

            return PartialView("_FilterResult", results);
        }

    }
}
