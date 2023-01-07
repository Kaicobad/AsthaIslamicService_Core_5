using AsthaIslamicService.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Repository.Interfaces
{
    public interface IPrayerTimeService
    {
        public Task<PrayerTimeViewModel> GetPrayerTime(string date);
        public Task<List<BDPlacesViewModel>> GetBDCities();
        public Task<PrayerTime> GetPrayerTime();
    }
}
