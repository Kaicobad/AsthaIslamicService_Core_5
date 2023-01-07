using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.CacheManager
{
    public class QuranCacheManager : IQuranCacheManager
    {
        private const string KEY = "Quran_Data";
        private readonly IMemoryCache memoryCache;
        private readonly ISurahService surahService;

        public QuranCacheManager(IMemoryCache memoryCache, ISurahService surahService)
        {
            this.memoryCache = memoryCache;
            this.surahService = surahService;
        }

        private async Task AddToCacheAsync(List<SurahModel_Quran> suraList)
        {
            var option = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(3),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),

            };


            memoryCache.Set<List<SurahModel_Quran>>(KEY, suraList, option);
        }
        public async Task<List<SurahModel_Quran>> GetCachedSura()
        {
            List<SurahModel_Quran> suraList = new List<SurahModel_Quran>();
            if (!memoryCache.TryGetValue(KEY, out suraList))
            {

                suraList = await surahService.GetAll();
                _ = AddToCacheAsync(suraList);
            }

            return suraList;
        }
        //public async Task<List<SurahModel>> GetCachedSura()
        //{
        //    List<SurahModel> suraList = new List<SurahModel>();
        //    if (!memoryCache.TryGetValue(KEY, out suraList))
        //    {

        //        suraList = await quranService.GetSurah();
        //        _ = AddToCacheAsync(suraList);
        //    }

        //    return suraList;
        //}
        //private async Task AddToCacheAsync(List<SurahModel> suraList)
        //{
        //    var option = new MemoryCacheEntryOptions
        //    {
        //        SlidingExpiration = TimeSpan.FromMinutes(3),
        //        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),

        //    };
        //    memoryCache.Set<List<SurahModel>>(KEY, suraList, option);
        //}

    }
}
