using AsthaIslamicService.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AsthaIslamicService.Controllers
{
    public class IslamicNameController : Controller
    {
        private readonly IIslamicNameService islamicNameService;

        public IslamicNameController(IIslamicNameService islamicNameService)
        {
            this.islamicNameService = islamicNameService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<PartialViewResult> GetNames(string type)
        {
            var data = await islamicNameService.GetAll(type);
            ViewBag.Wallpaper = data;
            return PartialView("_namePartial", data);
        }
    }
}
