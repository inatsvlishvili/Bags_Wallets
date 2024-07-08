using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bags_Wallets.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDbContext _DbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderRepository(ShopDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _DbContext = dbContext;
            _userManager = userManager;

        }
        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(string userId)
        {
            return await _DbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.ApplicationUserId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _DbContext.Orders
                .Include(x => x.ApplicationUser)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(o => o.Images)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _DbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(w => w.Product.Images)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            _DbContext.Orders.Add(order);
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _DbContext.Orders.Update(order);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _DbContext.Orders.FindAsync(id);
            if (order != null)
            {
                _DbContext.Orders.Remove(order);
                await _DbContext.SaveChangesAsync();
            }
        }
    }
}