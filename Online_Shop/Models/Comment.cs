using System.ComponentModel.DataAnnotations.Schema;

namespace Bags_Wallets.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        //public string ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime CreatedateTime { get; set; }
    }
}
