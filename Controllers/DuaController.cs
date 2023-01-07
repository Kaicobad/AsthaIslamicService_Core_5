using AsthaIslamicService.CacheManager;
using AsthaIslamicService.CacheService;
using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Caching.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace AsthaIslamicService.Controllers
{
    public class DuaController : Controller
    {
        private readonly IDuaCacheManager duaCacheManager;
        private readonly IDuaService duaService;
        private readonly MemoryCacheService memoryCacheService;

        public DuaController(IDuaCacheManager duaCacheManager, IDuaService duaService)
        {
            this.duaCacheManager = duaCacheManager;
            this.duaService = duaService;
            memoryCacheService = new MemoryCacheService();
            //this.memoryCacheService = memoryCacheService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<PartialViewResult> GetDuaList()
        {
            var viewModel = new List<DuaDetailsModel>();


            if (memoryCacheService.Exists("CacheDua"))
            {
                viewModel = (List<DuaDetailsModel>)memoryCacheService.Get("CacheDua");
                ViewBag.duaList = viewModel;
            }
            else
            {

                viewModel = await duaService.GetAll();
                memoryCacheService.Set("CacheDua", viewModel);
                ViewBag.duaList = viewModel;
            }
            return PartialView("_duaPartial");
        }

        public async Task<IActionResult> Details(string id)
        {
            List<DuaDetailsModel> data = await duaService.GetAll();
            DuaDetailsModel aDua = new DuaDetailsModel();
            if (data.Count > 0)
            {
                aDua = data.FirstOrDefault(d => d.Id == id);
                ViewBag.title = aDua.Title + " |";
            }
            return View(aDua);
        }
    }
}
