using Bags_Wallets.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bags_Wallets.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Collection { get; set; }
        public string Description { get; set; }
        public Gender? Gender { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public bool? IsOnSale { get; set; }
        public int Quantity { get; set; }
        public int OrderCount { get; set; }
        public DateTime CreatedateTime { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
        public List<string> ExistingImagePaths { get; set; } = new List<string>();


        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

    }

}
