using AsthaIslamicService.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsthaIslamicService.CacheManager
{
    public interface IPrayerTimeCacheManager
    {
        Task<PrayerTimeViewModel> GetCachedData(string date);
        Task<List<BDPlacesViewModel>> GetCachedCityData();
    }
}
