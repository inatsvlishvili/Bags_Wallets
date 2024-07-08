using Bags_Wallets.Data;
using Bags_Wallets.Models;
using Bags_Wallets.Repository.Interface;
using Microsoft.EntityFrameworkCore;


namespace Bags_Wallets.Repository.Implementation
{
    public class ContacRepository : IContactRepository
    {
        private readonly ShopDbContext _DbContext;
        public ContacRepository(ShopDbContext dbContext)
        {

            _DbContext = dbContext;

        }

        public async Task<ContactInfo> GetContactInfoByIdAsync(int id)
        {
            return await _DbContext.ContactInfos.FindAsync(id);
        }

        public async Task<IEnumerable<ContactInfo>> GetAllContactInfoAsync()
        {
            return await _DbContext.ContactInfos.ToListAsync();
        }

        public async Task AddContactInfoAsync(ContactInfo ContactInfo)
        {
            await _DbContext.ContactInfos.AddAsync(ContactInfo);
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateContactInfoAsync(ContactInfo ContactInfo)
        {
            _DbContext.ContactInfos.Update(ContactInfo);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteContactInfoAsync(int id)
        {
            var ContactInfo = await _DbContext.ContactInfos.FindAsync(id);
            if (ContactInfo != null)
            {
                _DbContext.ContactInfos.Remove(ContactInfo);
                await _DbContext.SaveChangesAsync();
            }
        }

        public async Task<AboutUs> GetAboutUsByIdAsync(int id)
        {
            return await _DbContext.AboutUs.FindAsync(id);
        }

        public async Task<IEnumerable<AboutUs>> GetAllAboutUsAsync()
        {
            return await _DbContext.AboutUs.ToListAsync();
        }

        public async Task AddAboutUsAsync(AboutUs aboutUs)
        {
            await _DbContext.AboutUs.AddAsync(aboutUs);
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateAboutUsAsync(AboutUs aboutUs)
        {
            _DbContext.AboutUs.Update(aboutUs);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteAboutUsAsync(int id)
        {
            var AboutUs = await _DbContext.AboutUs.FindAsync(id);
            if (AboutUs != null)
            {
                _DbContext.AboutUs.Remove(AboutUs);
                await _DbContext.SaveChangesAsync();
            }
        }

        public async Task<ContactUs> GetContactUsByIdAsync(int id)
        {
            return await _DbContext.ContactUs.FindAsync(id);
        }

        public async Task<IEnumerable<ContactUs>> GetAllContactUsAsync()
        {
            return await _DbContext.ContactUs.ToListAsync();
        }

        public async Task AddContactUsAsync(ContactUs contactUs)
        {
            contactUs.CreatedateTime = DateTime.Now;
            await _DbContext.ContactUs.AddAsync(contactUs);
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateContactUsAsync(ContactUs contactUs)
        {
            _DbContext.ContactUs.Update(contactUs);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteContactUsAsync(int id)
        {
            var ContactUs = await _DbContext.ContactUs.FindAsync(id);
            if (ContactUs != null)
            {
                _DbContext.ContactUs.Remove(ContactUs);
                await _DbContext.SaveChangesAsync();
            }
        }

        public async Task<Comment> GetCommentByIdAsync(int commentid)
        {
            return await _DbContext.Comments.FindAsync(commentid);
        }

        public async Task<IEnumerable<Comment>> GetAllCommentAsync()
        {
            return await _DbContext.Comments.ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _DbContext.Comments.AddAsync(comment);
            await _DbContext.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _DbContext.Comments.Update(comment);
            await _DbContext.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentid)
        {
            var Comment = await _DbContext.Comments.FindAsync(commentid);
            if (Comment != null)
            {
                _DbContext.Comments.Remove(Comment);
                await _DbContext.SaveChangesAsync();
            }
        }

    }
}