using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.CacheManager
{
    public class PrayerTimeCacheManager : IPrayerTimeCacheManager
    {
        private const string KEY = "PrayerTime_Data";
        private const string KEYCITY = "CITY_Data";
        private readonly IMemoryCache memoryCache;
        private readonly IPrayerTimeService prayerTimeService;

        public PrayerTimeCacheManager(IMemoryCache memoryCache, IPrayerTimeService prayerTimeService)
        {
            this.memoryCache = memoryCache;
            this.prayerTimeService = prayerTimeService;
        }
        private async Task AddToCacheCityAsync(List<BDPlacesViewModel> alist)
        {
            var option = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(3),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),

            };


            memoryCache.Set<List<BDPlacesViewModel>>(KEYCITY, alist, option);
        }
        public async Task<List<BDPlacesViewModel>> GetCachedCityData()
        {
            List<BDPlacesViewModel> cityList = new List<BDPlacesViewModel>();
            if (!memoryCache.TryGetValue(KEYCITY, out cityList))
            {

                cityList = await prayerTimeService.GetBDCities();
                _ = AddToCacheCityAsync(cityList);
            }

            return cityList;
        }

        private async Task AddToCacheAsync(PrayerTimeViewModel alist)
        {
            var option = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(3),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6),

            };
            memoryCache.Set<PrayerTimeViewModel>(KEY, alist, option);
        }

        public async Task<PrayerTimeViewModel> GetCachedData(string date)
        {

            string currentDate = DateTime.Now.ToString("%d-MMM");
            PrayerTimeViewModel duaList = new PrayerTimeViewModel();
            if (!memoryCache.TryGetValue(KEY, out duaList) || currentDate != date)
            {

                duaList = await prayerTimeService.GetPrayerTime(date);
                _ = AddToCacheAsync(duaList);
            }

            return duaList;
        }
    }
}
