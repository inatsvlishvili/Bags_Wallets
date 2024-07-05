using Bags_Wallets.Models;

namespace Bags_Wallets.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
