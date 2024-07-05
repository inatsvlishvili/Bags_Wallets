using Bags_Wallets.Models;
using System.ComponentModel.DataAnnotations;

namespace Bags_Wallets.ViewModels
{
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
