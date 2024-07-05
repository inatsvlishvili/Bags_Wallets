using Bags_Wallets.Controllers;
using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bags_Wallets.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Bags_Wallets.Repository.Implementation
{
    public class PageImageCategoryRepository : IPageImageCategoryRepository
    {
        private readonly ShopDbContext _DbContext;
        private readonly IWebHostEnvironment _webHost;

        public PageImageCategoryRepository(ShopDbContext dbContext, IWebHostEnvironment webHost)
        {
            _webHost = webHost;
            _DbContext = dbContext;

        }
        public async Task<IEnumerable<PageImageCategory>> GetAllAsync()
        {
            return await _DbContext.PageImageCategories.ToListAsync();
        }

        public async Task<PageImageCategory> GetByIdAsync(int id)
        {
            return await _DbContext.PageImageCategories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(PageImageCategory category)
        {
            _DbContext.PageImageCategories.Add(category);
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PageImageCategory category)
        {
            _DbContext.PageImageCategories.Update(category);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _DbContext.PageImageCategories.FindAsync(id);
            if (category != null)
            {
                _DbContext.PageImageCategories.Remove(category);
                await _DbContext.SaveChangesAsync();
            }
        }
    }
}