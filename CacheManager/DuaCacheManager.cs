using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsthaIslamicService.CacheManager
{
    public class DuaCacheManager : IDuaCacheManager
    {
        private const string KEY = "Dua_Data";
        private readonly IMemoryCache memoryCache;
        private readonly IDuaService duaService;

        public DuaCacheManager(IMemoryCache memoryCache, IDuaService duaService)
        {
            this.memoryCache = memoryCache;
            this.duaService = duaService;
        }
        public async Task<List<DuaDetailsModel>> GetCachedDua()
        {
            List<DuaDetailsModel> duaList = new List<DuaDetailsModel>();
            if (!memoryCache.TryGetValue(KEY, out duaList))
            {

                duaList = await duaService.GetAll();
                _ = AddToCacheAsync(duaList);
            }

            return duaList;
        }
        private async Task AddToCacheAsync(List<DuaDetailsModel> duaList)
        {
            var option = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(3),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),

            };


            memoryCache.Set<List<DuaDetailsModel>>(KEY, duaList, option);
        }
    }
}
