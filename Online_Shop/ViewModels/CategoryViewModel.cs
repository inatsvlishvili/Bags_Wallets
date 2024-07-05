using Bags_Wallets.Models;

namespace Bags_Wallets.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleGeo { get; set; }
        public ICollection<Product> Items { get; set; }
        public string Categoryname { get; set; }
    }
}
