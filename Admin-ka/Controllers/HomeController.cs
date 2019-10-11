using Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View(new StatViewModel());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
