using Microsoft.AspNetCore.Mvc;

namespace Work_03.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
