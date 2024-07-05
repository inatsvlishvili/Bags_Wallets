using Bags_Wallets.Models;

namespace Bags_Wallets.Repository.Interface
{
    public interface ISellerRepository
    {
        Task<IEnumerable<Seller>> GetAllSellersAsync();
        Task<Seller> GetSellerByIdAsync(int id);
        Task AddSellerAsync(Seller seller);
        Task UpdateSellerAsync(Seller seller);
        Task DeleteSellerAsync(int id);
    }
}
