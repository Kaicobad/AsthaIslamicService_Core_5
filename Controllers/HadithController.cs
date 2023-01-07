using AsthaIslamicService.CacheManager;
using AsthaIslamicService.CacheService;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Controllers
{
    public class HadithController : Controller
    {
        private readonly IHadithService hadithService;
        private readonly IHadithCacheManager hadithCacheManager;

        public HadithController(IHadithService hadithService, IHadithCacheManager hadithCacheManager)
        {
            this.hadithService = hadithService;
            this.hadithCacheManager = hadithCacheManager;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<PartialViewResult> GetHadithData()
        {
            string cacheKey = CacheEnum.AllHadith;
            MemoryCacheService memoryCacheService = new MemoryCacheService();
            //CacheDataLoad _serviceCache = new CacheDataLoad();
            var subsList = new List<HadithViewModel>();
            if (memoryCacheService.Exists(cacheKey))
            {
                subsList = (List<HadithViewModel>)memoryCacheService.Get(cacheKey);
            }
            if (subsList.Count == 0)
            {
                subsList = await hadithService.GetAll();
                memoryCacheService.Set(cacheKey, subsList);
            }
            _ = Task.Run(async () =>
            {
                if (memoryCacheService.IfExpireThenSet(cacheKey))
                {
                    memoryCacheService.Set(cacheKey, await hadithService.GetAll());
                }
            });

            var data = subsList;
            ViewBag.hadithList = data;
            return PartialView("_partialHadith");
        }
    }
}
