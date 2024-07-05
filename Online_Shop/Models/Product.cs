using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Bags_Wallets.Models
{
    public class Product
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
        //public int OrderCount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public DateTime CreatedateTime { get; set; }
       

    }
    public enum Gender
    {
        [Description("ქალი")]
        Woman,
        [Description("კაცი")]
        Man,
        [Description("ბავშვი")]
        Kid
    }

}
