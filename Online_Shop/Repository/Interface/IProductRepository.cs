using Bags_Wallets.Models;
using Bags_Wallets.ViewModels;

namespace Bags_Wallets.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetFilteredBagssAsync(ProductListViewModel filter);
        Task<IEnumerable<Product>> GetFilteredWalletsAsync(ProductListViewModel filter);
        Task<IEnumerable<Product>> GetFilteredSaleProductAsync(ProductListViewModel filter);
        Task<IEnumerable<Product>> GetBagAsync();
        Task<IEnumerable<Product>> GetWalletAsync();
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<(IEnumerable<Product> Products, int TotalCount)> GetFilteredBagsAsync(Gender? gender, double? minPrice, double? maxPrice, string sortBy, int pageNumber, int pageSize);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetFilteredWalletsAsync(Gender? gender, double? minPrice, double? maxPrice, string sortBy, int pageNumber, int pageSize);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Product>> GetAllAsync(int pageIndex, int pageSize);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllBagAsync(int pageNumber, int pageSize);


        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllWalletAsync(int pageNumber, int pageSize);
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<int> GetTotalProductCount();

    }
}
