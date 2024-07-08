using Microsoft.AspNetCore.Mvc;
using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;
using Bags_Wallets.Repository.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Bags_Wallets.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopDbContext _DbContext;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;


        public ProductController(ShopDbContext dbContext, IProductRepository productRepository,
            IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _DbContext = dbContext;
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }


        public async Task<IActionResult> Product(int pageIndex = 1, int pageSize = 10)
        {
            ViewBag.Photo = await _DbContext.Products.Include(x => x.Images).FirstOrDefaultAsync();

            var totalProducts = await _productRepository.GetTotalProductCount();
            var products = await _productRepository.GetAllAsync(pageIndex, pageSize);

            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);

            var viewModel = new ProductListViewModel
            {
                Products = productViewModels,
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize),
                CurrentPage = pageIndex
            };

            ViewBag.Pages = viewModel;

            return View(productViewModels);

        }

        public async Task<IActionResult> AddProduct()
        {
            ViewBag.Seller = await _DbContext.Sellers.ToListAsync();
            ViewBag.Category = await _DbContext.Categories.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel viewModel)
        {
            ViewBag.Seller = await _DbContext.Sellers.ToListAsync();
            ViewBag.Category = await _DbContext.Categories.ToListAsync();


            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(viewModel);

                if (viewModel.ImageFiles != null && viewModel.ImageFiles.Any())
                {
                    foreach (var file in viewModel.ImageFiles)
                    {
                        string uniqueFileName = null;
                        var FileDic = "Images/product/";
                        string imgPath = Path.Combine(_webHostEnvironment.WebRootPath, FileDic);
                        if (!Directory.Exists(imgPath))
                            Directory.CreateDirectory(imgPath);
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, FileDic);
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        product.Images.Add(new ProductImage { ImagePath = "/images/product/" + uniqueFileName });
                    }
                }
                product.CreatedateTime = DateTime.Now;

                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Product));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            ViewBag.Seller = _DbContext.Sellers.ToListAsync();
            ViewBag.Category = _DbContext.Categories.ToListAsync();

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ProductViewModel>(product);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, ProductViewModel viewModel)
        {
            ViewBag.Seller = _DbContext.Sellers.ToList();
            ViewBag.Category = _DbContext.Categories.ToList();

            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                _mapper.Map(viewModel, product);

                if (viewModel.ImageFiles != null && viewModel.ImageFiles.Any())
                {
                    foreach (var file in viewModel.ImageFiles)
                    {
                        string uniqueFileName = null;
                        var FileDic = "Images/product/";
                        string imgPath = Path.Combine(_webHostEnvironment.WebRootPath, FileDic);
                        if (!Directory.Exists(imgPath))
                            Directory.CreateDirectory(imgPath);
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, FileDic);
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        product.Images.Add(new ProductImage { ImagePath = "/images/product/" + uniqueFileName });
                    }
                }

                await _productRepository.UpdateAsync(product);
                return RedirectToAction(nameof(Product));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ProductViewModel>(product);
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductViewModel viewModel)
        {
            var product = await _productRepository.GetByIdAsync(viewModel.Id);

            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(product.Id);
            return RedirectToAction(nameof(Product));

        }
        public async Task<IActionResult> SearchProduct(string query)
        {

            if (string.IsNullOrEmpty(query))
            {
                return PartialView("_SearchResults", await _DbContext.Products.Include(x => x.Seller)
                .Include(x => x.Category).Include(p => p.Images)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Collection = p.Collection,
                    Gender = p.Gender,
                    Category = p.Category,
                    Seller = p.Seller,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Discount = p.Discount,
                    IsOnSale = p.IsOnSale,
                    Description = p.Description,
                    CreatedateTime = p.CreatedateTime,
                    Images = p.Images
                })
                .ToListAsync());
            }

            var results = await _DbContext.Products.Include(x => x.Seller)
                .Include(x => x.Category).Include(p => p.Images)
                .Where(p => p.Title.Contains(query))
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Collection = p.Collection,
                    Gender = p.Gender,
                    Category = p.Category,
                    Seller = p.Seller,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Discount = p.Discount,
                    IsOnSale = p.IsOnSale,
                    Description = p.Description,
                    CreatedateTime = p.CreatedateTime,
                    Images = p.Images
                })
                .ToListAsync();

            return PartialView("_SearchResults", results);
        }

    }

}
