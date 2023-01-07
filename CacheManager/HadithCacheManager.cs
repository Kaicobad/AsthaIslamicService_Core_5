using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.Repository.Services;
using AsthaIslamicService.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.CacheManager
{
    public class HadithCacheManager : IHadithCacheManager
    {
        private readonly IMemoryCache memoryCache;
        private readonly IHadithService hadithService;

        private const string KEY = "Hadis_Data";

        public HadithCacheManager(IMemoryCache memoryCache, IHadithService hadithService)
        {
            this.memoryCache = memoryCache;
            this.hadithService = hadithService;
        }

        private async Task AddToCacheAsync(List<HadithViewModel> hadithList)
        {
            var option = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(3),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),

            };
            memoryCache.Set<List<HadithViewModel>>(KEY, hadithList, option);
        }
        public async Task<List<HadithViewModel>> GetCachedHadith()
        {
            List<HadithViewModel> hadisList = new List<HadithViewModel>();
            if (!memoryCache.TryGetValue(KEY, out hadisList))
            {

                hadisList = await hadithService.GetAll();
                _ = AddToCacheAsync(hadisList);
            }

            return hadisList;
        }
    }
}
