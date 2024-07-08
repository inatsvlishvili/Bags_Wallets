using Bags_Wallets.Models;

namespace Bags_Wallets.Repository.Interface
{
    public interface IPageImageCategoryRepository
    {
        Task<IEnumerable<PageImageCategory>> GetAllAsync();
        Task<PageImageCategory> GetByIdAsync(int id);
        Task AddAsync(PageImageCategory category);
        Task UpdateAsync(PageImageCategory category);
        Task DeleteAsync(int id);
    }
}
