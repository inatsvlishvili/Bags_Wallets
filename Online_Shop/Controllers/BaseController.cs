using Bags_Wallets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Bags_Wallets.Controllers
{
    public class BaseController : Controller
    {
        public ShopDbContext _DbContext;

        public BaseController(ShopDbContext DbContext)

        {
            _DbContext = DbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ContactInfo = _DbContext.ContactInfos.FirstOrDefault();
            ViewBag.Gender = _DbContext.Products.Include(x => x.Gender).ToListAsync();
           
            base.OnActionExecuting(filterContext);
        }
    }
}
