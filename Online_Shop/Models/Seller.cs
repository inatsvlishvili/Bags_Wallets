using System.ComponentModel.DataAnnotations.Schema;

namespace Bags_Wallets.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime CreatedateTime { get; set; }


    }
}
