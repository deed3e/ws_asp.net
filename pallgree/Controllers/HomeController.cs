using Microsoft.AspNetCore.Mvc;

namespace pallgree.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
