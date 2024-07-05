using Bags_Wallets.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bags_Wallets.ViewModels
{
    public class PageImageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Collection { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { set; get; }
        public string PageImageCategoryName { get; set; }
        public int PageImageCategoryId { get; set; }
        public PageImageCategory PageImageCategory { get; set; }
        public DateTime CreatedateTime { get; set; }

    }
}
