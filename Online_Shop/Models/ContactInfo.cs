using System.ComponentModel.DataAnnotations;

namespace Bags_Wallets.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Tiktok { get; set; }
        
    }
}
