using Microsoft.AspNetCore.Mvc;

namespace AsthaIslamicService.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
