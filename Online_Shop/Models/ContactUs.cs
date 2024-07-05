using System.ComponentModel.DataAnnotations;

namespace Bags_Wallets.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Text { get; set; }
        public DateTime CreatedateTime { get; set; }
    }
}
