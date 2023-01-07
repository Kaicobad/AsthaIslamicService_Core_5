using AsthaIslamicService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Controllers
{
    public class ZakatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> New()
        {


            return View();
        }

        [HttpPost]

        public ActionResult New(ZakatModel aModel)
        {
            ViewBag.ZakatRecord = aModel;
            return View();
        }

    }
}
