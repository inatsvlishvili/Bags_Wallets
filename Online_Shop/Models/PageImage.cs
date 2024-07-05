using System.ComponentModel.DataAnnotations.Schema;

namespace Bags_Wallets.Models
{
    public class PageImage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Collection { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageFile { set; get; }
        public int PageImageCategoryId { get; set; }
        public PageImageCategory PageImageCategory { get; set; }
        public DateTime CreatedateTime { get; set; }

    }
}
