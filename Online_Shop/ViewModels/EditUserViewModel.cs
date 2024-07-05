using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bags_Wallets.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            //Roles = new List<string>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { set; get; }
        public string Role { get; set; }
    }
}
