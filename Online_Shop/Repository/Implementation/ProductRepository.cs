using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Bags_Wallets.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;

namespace Bags_Wallets.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly ShopDbContext _DbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductRepository(ShopDbContext dbContext, IWebHostEnvironment webHostEnvironment, ILogger<ProductRepository> logger)
        {
            _logger = logger;
            _DbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task<IEnumerable<Product>> GetFilteredBagssAsync(ProductListViewModel filter)
        {
            var query = _DbContext.Products.Where(x=>x.Category.Title == "Bag").Include(x => x.Images).AsQueryable();

            if (filter.Gender.HasValue)
            {
                query = query.Where(p => p.Gender == filter.Gender.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }
            if (filter.IsOnSale.HasValue)
            {
                query = query.Where(p => p.IsOnSale == filter.IsOnSale.Value);
            }
           
            return await query.ToListAsync();
        }
        
        public async Task<IEnumerable<Product>> GetFilteredWalletsAsync(ProductListViewModel filter)
        {
            var query = _DbContext.Products.Where(x => x.Category.Title == "Wallet").Include(x => x.Images).AsQueryable();

            if (filter.Gender.HasValue)
            {
                query = query.Where(p => p.Gender == filter.Gender.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }
            if (filter.IsOnSale.HasValue)
            {
                query = query.Where(p => p.IsOnSale == filter.IsOnSale.Value);
            }
           
            return await query.ToListAsync();
        } 
        public async Task<IEnumerable<Product>> GetFilteredSaleProductAsync(ProductListViewModel filter)
        {
            var query = _DbContext.Products.Where(x => x.IsOnSale == true).Include(x => x.Images).Take(30).AsQueryable();

            if (filter.Gender.HasValue)
            {
                query = query.Where(p => p.Gender == filter.Gender.Value);
            }
                      

            //if (filter.IsOnSale.HasValue)
            //{
            //    query = query.Where(p => p.IsOnSale == filter.IsOnSale.Value);
            //}
           
            return await query.ToListAsync();
        }




        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetFilteredWalletsAsync(Gender? Gender, double? minPrice, double? maxPrice, string sortBy, int pageNumber, int pageSize)
        {
            var query = _DbContext.Products.AsQueryable();

            if (Gender.HasValue)
            {
                query = query.Where(p => p.Gender == Gender);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            switch (sortBy)
            {
                case "price":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "added":
                    query = query.OrderByDescending(p => p.CreatedateTime);
                    break;
                //case "ordered":
                //    query = query.OrderByDescending(p => p.OrderCount);
                //    break;
                case "sale":
                    query = query.OrderByDescending(p => p.IsOnSale);
                    break;
                default:
                    query = query.OrderBy(p => p.Title);
                    break;
            }

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetFilteredBagsAsync(Gender? Gender, double? minPrice, double? maxPrice, string sortBy, int pageNumber, int pageSize)
        {
            var query = _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images)
                .Where(x => x.Category.Title == "Wallet").AsQueryable();

            if (Gender.HasValue)
            {
                query = query.Where(p => p.Gender == Gender);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            switch (sortBy)
            {
                case "price":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "added":
                    query = query.OrderByDescending(p => p.CreatedateTime);
                    break;
                //case "ordered":
                //    query = query.OrderByDescending(p => p.OrderCount);
                //    break;
                case "sale":
                    query = query.OrderByDescending(p => p.IsOnSale);
                    break;
                default:
                    query = query.OrderBy(p => p.Title);
                    break;
            }

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _DbContext.Products.CountAsync();
            var products = await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images)
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

            return (products, totalCount);
        }
        public async Task<IEnumerable<Product>> GetAllAsync(int pageIndex, int pageSize)
        {
            return await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images)
                .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).ToListAsync();

        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images).ToListAsync();

        }
        
        public async Task<IEnumerable<Product>> GetBagAsync()
        {
            return await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category)
                .Include(p => p.Images).Where(x=>x.Category.Title == "Bag").ToListAsync();

        }
        public async Task<IEnumerable<Product>> GetWalletAsync()
        {
            return await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images)
                .Where(x => x.Category.Title == "Wallet").ToListAsync();

        }
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetAllBagAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _DbContext.Products.CountAsync();
            var products = await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images)
                .Where(x => x.Category.Title == "Bag")
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();
            return (products, totalCount);

            //return await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images)
            //    .Where(x => x.Category.Title == "Bag").ToListAsync();
        }
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetAllWalletAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _DbContext.Products.CountAsync();
            var products = await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images)
                .Where(x => x.Category.Title == "Wallet")
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

            return (products, totalCount);

            //return await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images)
            //    .Where(x => x.Category.Title == "Wallet").ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {

            return await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(x => x.Comments)
                .Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            _DbContext.Products.Add(product);
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _DbContext.Products.Update(product);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _DbContext.Products.Include(x => x.Seller).Include(x => x.Category).Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _DbContext.Products.Remove(product);
                await _DbContext.SaveChangesAsync();
            }
        }
        public async Task<int> GetTotalProductCount()
        {
            return await _DbContext.Products.CountAsync();
        }
    }
}
