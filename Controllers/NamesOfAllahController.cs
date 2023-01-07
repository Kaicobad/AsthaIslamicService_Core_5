using AsthaIslamicService.CacheService;
using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Controllers
{
    public class NamesOfAllahController : Controller
    {
        private readonly ITokenService tokenService;
        private readonly INameOfAllahService nameOfAllahService;

        public NamesOfAllahController(ITokenService tokenService, INameOfAllahService nameOfAllahService)
        {
            this.tokenService = tokenService;
            this.nameOfAllahService = nameOfAllahService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Index2()
        {
            return View();
        }

        public async Task<PartialViewResult> GetNameofAllah()
        {
            //NameOfAllahService service = new NameOfAllahService();
            string cacheKey = CacheEnum.NameofAllah;
            MemoryCacheService memoryCache = new MemoryCacheService();
            if (memoryCache.Exists(cacheKey))
            {
                var data = (List<NameOfAllahViewModel>)memoryCache.Get(cacheKey);
                return PartialView("_partialNamesOfAllah", data);
            }
            else
            {
                var data = await nameOfAllahService.GetNames();
                memoryCache.Set(cacheKey, data);
                return PartialView("_partialNamesOfAllah", data);
            }
        }
        public async Task<PartialViewResult> GetNameofAllah2()
        {
            //NameOfAllahService service = new NameOfAllahService();
            string cacheKey = CacheEnum.NameofAllah;
            MemoryCacheService memoryCache = new MemoryCacheService();
            if (memoryCache.Exists(cacheKey))
            {
                var data = (List<NameOfAllahViewModel>)memoryCache.Get(cacheKey);
                return PartialView("_NamesOfAllah", data);
            }
            else
            {
                var data = await nameOfAllahService.GetNames();
                memoryCache.Set(cacheKey, data);
                return PartialView("_NamesOfAllah", data);
            }
        }
    }
}
