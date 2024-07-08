using Bags_Wallets.Models;

namespace Bags_Wallets.Repository.Interface
{
    public interface IPageImageRepository
    {
        Task<IEnumerable<PageImage>> GetAllAsync();
        Task<PageImage> GetByIdAsync(int id);
        Task AddAsync(PageImage pageImage);
        Task UpdateAsync(PageImage pageImage);
        Task DeleteAsync(int id);
    }
}
