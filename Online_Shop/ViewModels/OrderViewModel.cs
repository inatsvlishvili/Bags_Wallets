using Bags_Wallets.Models;
using System.ComponentModel.DataAnnotations;

namespace Bags_Wallets.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }


    }
}
