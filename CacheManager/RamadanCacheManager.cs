using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.Repository.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.CacheManager
{
    public class RamadanCacheManager : IRamadanCacheManager
    {
        private const string KEY = "Ramadan_Data";

        private readonly IMemoryCache memoryCache;
        private readonly IRamadanService ramadanService;

        public RamadanCacheManager(IMemoryCache memoryCache, IRamadanService ramadanService)
        {
            this.memoryCache = memoryCache;
            this.ramadanService = ramadanService;
        }
        //public async Task<List<DuaModel>> GetCachedData()
        //{
        //    List<DuaModel> duaList = new List<DuaModel>();
        //    if (!memoryCache.TryGetValue(KEY, out duaList))
        //    {

        //        duaList = await ramadanService.GetRamadanDua();
        //        _ = AddToCacheAsync(duaList);
        //    }

        //    return duaList;
        //}
        private async Task AddToCacheAsync(List<DuaModel> duaList)
        {
            var option = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(3),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),
            };
            memoryCache.Set<List<DuaModel>>(KEY, duaList, option);
        }
    }
}
