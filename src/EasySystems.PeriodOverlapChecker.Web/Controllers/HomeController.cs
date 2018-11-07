using Microsoft.AspNetCore.Mvc;

namespace EasySystems.PeriodOverlapChecker.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}