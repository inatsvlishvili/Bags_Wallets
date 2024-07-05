using Bags_Wallets.Models;

namespace Bags_Wallets.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public Gender? Gender { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public bool? OrderByOrder { get; set; }
        public bool? IsOnSale { get; set; }
        public DateTime? CreatedateTime { get; set; }



    }
}
