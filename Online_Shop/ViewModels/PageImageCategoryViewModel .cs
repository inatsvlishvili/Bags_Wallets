using Bags_Wallets.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bags_Wallets.ViewModels
{
    public class PageImageCategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleGEO { get; set; }
        public string PageImageCateName { get; set; }

    }
}
