using System.ComponentModel.DataAnnotations;

namespace Bags_Wallets.ViewModels
{
    public class ContactUsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Text { get; set; }
        public DateTime CreatedateTime { get; set; }
    }
}
