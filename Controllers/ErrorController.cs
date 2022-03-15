using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
