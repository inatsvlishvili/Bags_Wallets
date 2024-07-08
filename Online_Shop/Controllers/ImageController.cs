using Bags_Wallets.Data;
using Bags_Wallets.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;
using AutoMapper;

namespace Bags_Wallets.Controllers
{
    public class ImageController : Controller
    {
        private readonly ShopDbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPageImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageController(ShopDbContext dbContext, IWebHostEnvironment webHostEnvironment, IPageImageRepository imageRepository,
           IMapper mapper)
        {
            _DbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> PageImage()
        {
            var pageImage = await _imageRepository.GetAllAsync();
            var viewModel = _mapper.Map<IEnumerable<PageImageViewModel>>(pageImage);
            return View(viewModel);

        }

        public async Task<IActionResult> AddPageImage()
        {
            ViewBag.pageImageCategory = await _DbContext.PageImageCategories.ToListAsync();
            ViewBag.product = await _DbContext.Products.ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPageImage(PageImageViewModel model)
        {
            ViewBag.pageImageCategory = await _DbContext.PageImageCategories.ToListAsync();
            ViewBag.product = await _DbContext.Products.ToListAsync();

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.ImageFile != null)
                {

                    var FileDic = "Images/PageImages/";
                    string imgPath = Path.Combine(_webHostEnvironment.WebRootPath, FileDic);
                    if (!Directory.Exists(imgPath))
                        Directory.CreateDirectory(imgPath);
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, FileDic);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    //using (FileStream fileStream = System.IO.File.Create(filePath))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }
                }

                model.CreatedateTime = DateTime.Now;
                model.ImageName = uniqueFileName;
                var pageImage = _mapper.Map<PageImage>(model);


                await _imageRepository.AddAsync(pageImage);
                return RedirectToAction("PageImage");
            }

            return View(model);
        }

        public async Task<IActionResult> EditPageImage(int id)
        {
            var pageImage = await _imageRepository.GetByIdAsync(id);
            ViewBag.pageImageCategory = await _DbContext.PageImageCategories.ToListAsync();

            if (pageImage == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<PageImageViewModel>(pageImage);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPageImage(int id, PageImageViewModel model)
        {

            if (ModelState.IsValid)
            {
                var pageImage = await _imageRepository.GetByIdAsync(id);
                if (pageImage == null)
                {
                    return NotFound();
                }
                ViewBag.pageImageCategory = await _DbContext.PageImageCategories.ToListAsync();

                if (model.ImageFile != null)
                {
                    if (!string.IsNullOrEmpty(pageImage.ImageName))
                    {
                        var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/pageimages/", pageImage.ImageName);
                        System.IO.File.Delete(existingFilePath);
                    }

                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/pageimages/");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    pageImage.ImageName = uniqueFileName;
                }
                model.CreatedateTime = DateTime.Now;
                pageImage.Title = model.Title;

                await _imageRepository.UpdateAsync(pageImage);
                return RedirectToAction("PageImage");
            }

            return View(model);
        }

        public async Task<IActionResult> DeletePageImage(int id)
        {
            var pageImage = await _imageRepository.GetByIdAsync(id);
            if (pageImage == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<PageImageViewModel>(pageImage);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePageImage(PageImageViewModel viewModel)
        {
            var pageImage = await _imageRepository.GetByIdAsync(viewModel.Id);

            if (pageImage == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(pageImage.ImageName))
            {
                var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images/pageimages/", pageImage.ImageName);
                System.IO.File.Delete(existingFilePath);
            }

            await _imageRepository.DeleteAsync(pageImage.Id);
            return RedirectToAction("PageImage");
        }

        public async Task<IActionResult> Details(int id)
        {
            var pageImage = await _imageRepository.GetByIdAsync(id);
            if (pageImage == null)
            {
                return NotFound();
            }

            return View(pageImage);
        }
    }
}