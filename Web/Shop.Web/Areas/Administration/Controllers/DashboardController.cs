namespace Shop.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}