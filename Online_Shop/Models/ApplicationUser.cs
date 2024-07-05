using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bags_Wallets.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { set; get; }
        public DateTime CreatedateTime { get; set; }
        public ShoppingCartItem ShoppingCartItem { get; set; }

    }
}
