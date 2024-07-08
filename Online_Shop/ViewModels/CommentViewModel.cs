using Bags_Wallets.Models;

namespace Bags_Wallets.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime CreatedateTime { get; set; }
    }
}
