using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Bags_Wallets.Repository.Implementation
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ShopDbContext _DbContext;
        public SellerRepository(ShopDbContext dbContext)
        {
            _DbContext = dbContext;

        }
        public async Task<IEnumerable<Seller>> GetAllSellersAsync()
        {
            return await _DbContext.Sellers.ToListAsync();
        }

        public async Task<Seller> GetSellerByIdAsync(int id)
        {
            return await _DbContext.Sellers.FindAsync(id);
        }

        public async Task AddSellerAsync(Seller seller)
        {
            _DbContext.Sellers.Add(seller);
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateSellerAsync(Seller seller)
        {
            _DbContext.Entry(seller).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteSellerAsync(int id)
        {
            var seller = await _DbContext.Sellers.FindAsync(id);
            if (seller != null)
            {
                _DbContext.Sellers.Remove(seller);
                await _DbContext.SaveChangesAsync();
            }
        }
    }
}