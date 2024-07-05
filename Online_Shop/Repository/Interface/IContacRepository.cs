using Bags_Wallets.Models;

namespace Bags_Wallets.Repository.Interface
{
    public interface IContactRepository
    {
        Task<ContactInfo> GetContactInfoByIdAsync(int id);
        Task<IEnumerable<ContactInfo>> GetAllContactInfoAsync();
        Task AddContactInfoAsync(ContactInfo contactInfo);
        Task UpdateContactInfoAsync(ContactInfo contactInfo);
        Task DeleteContactInfoAsync(int id);

        Task<AboutUs> GetAboutUsByIdAsync(int id);
        Task<IEnumerable<AboutUs>> GetAllAboutUsAsync();
        Task AddAboutUsAsync(AboutUs aboutUs);
        Task UpdateAboutUsAsync(AboutUs aboutUs);
        Task DeleteAboutUsAsync(int id);

        Task<ContactUs> GetContactUsByIdAsync(int id);
        Task<IEnumerable<ContactUs>> GetAllContactUsAsync();
        Task AddContactUsAsync(ContactUs contactUs);
        Task UpdateContactUsAsync(ContactUs contactUs);
        Task DeleteContactUsAsync(int id);

        Task<Comment> GetCommentByIdAsync(int commentid);
        Task<IEnumerable<Comment>> GetAllCommentAsync();
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int commentid);

        
    }
}
