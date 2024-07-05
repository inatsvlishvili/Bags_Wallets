using Bags_Wallets.Models;
using System.ComponentModel.DataAnnotations;

namespace Bags_Wallets.ViewModels
{
    public class ShoppingCartItemViewModel
    {
        public int Id { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Image { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

    }
}
