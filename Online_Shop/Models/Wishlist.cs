namespace Bags_Wallets.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}
