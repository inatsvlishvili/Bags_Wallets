using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Bags_Wallets.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleGeo { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
