using Bags_Wallets.Models;
using Microsoft.AspNetCore.Identity;

namespace Bags_Wallets.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<IdentityResult> AddAsync(ApplicationUser user, string password);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string id);
    }
}
