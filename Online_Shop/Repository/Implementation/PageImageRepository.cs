using Bags_Wallets.Controllers;
using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bags_Wallets.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;


namespace Bags_Wallets.Repository.Implementation
{
    public class PageImageRepository : IPageImageRepository
    {
        private readonly ShopDbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public PageImageRepository(ShopDbContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _DbContext = dbContext;
            _mapper = mapper;

        }
        public async Task<IEnumerable<PageImage>> GetAllAsync()
        {
            //var categoryName = _DbContext.PageImages.Select(model => new PageImageViewModel()
            //{
            //    PageImageCategoryName = model.PageImageCategory.TitleGEO

            //}).ToListAsync();

            return await _DbContext.PageImages.Include(x=>x.PageImageCategory).ToListAsync();
        }

        public async Task<PageImage> GetByIdAsync(int id)
        {
            return await _DbContext.PageImages.FindAsync(id);
        }

        public async Task AddAsync(PageImage pageImage)
        {
            _DbContext.PageImages.Add(pageImage);
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PageImage pageImage)
        {
            _DbContext.PageImages.Update(pageImage);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pageImage = await _DbContext.PageImages.FindAsync(id);
            if (pageImage != null)
            {
                _DbContext.PageImages.Remove(pageImage);
                await _DbContext.SaveChangesAsync();
            }
        }
    }
}