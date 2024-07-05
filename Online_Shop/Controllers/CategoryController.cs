using Bags_Wallets.Data;
using Bags_Wallets.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Bags_Wallets.Models;
using AutoMapper;


namespace Bags_Wallets.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShopDbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPageImageCategoryRepository _pageImageCategoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(IWebHostEnvironment webHostEnvironment, ILogger<CategoryController> logger,
            ShopDbContext dbContext, IPageImageCategoryRepository PageImageCategoryRepository, IMapper mapper)
        {
            _DbContext = dbContext;
            _pageImageCategoryRepository = PageImageCategoryRepository;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _pageImageCategoryRepository.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PageImageCategory category)
        {
            if (ModelState.IsValid)
            {
                await _pageImageCategoryRepository.AddAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _pageImageCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PageImageCategory category)
        {
            if (ModelState.IsValid)
            {
                await _pageImageCategoryRepository.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _pageImageCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pageImageCategoryRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _pageImageCategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    }
}