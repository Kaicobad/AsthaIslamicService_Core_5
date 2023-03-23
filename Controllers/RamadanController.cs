using AsthaIslamicService.CacheManager;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Linq;

namespace AsthaIslamicService.Controllers
{
    public class RamadanController : Controller
    {

        private readonly IRamadanService ramadanService;
        private readonly IPrayerTimeService prayerTimeService;

        public RamadanController( IRamadanService ramadanService,IPrayerTimeService prayerTimeService)
        {
           
            this.ramadanService = ramadanService;
            this.prayerTimeService = prayerTimeService;
        }
        public async Task<IActionResult> Index()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("bn-BD");
            ViewBag.CurrentDate = DateTime.Now.ToLongDateString();
            var data = await prayerTimeService.GetPrayerTime();
            var ramadandata = await ramadanService.RamadanTimeFromALAdhan("Dhaka");
            if (data != null)
            {
                data.data.timings.Fajr = Convert.ToDateTime(data.data.timings.Fajr).ToString("t");
                data.data.timings.Dhuhr = Convert.ToDateTime(data.data.timings.Dhuhr).ToString("t");
                data.data.timings.Asr = Convert.ToDateTime(data.data.timings.Asr).ToString("t");
                data.data.timings.Sunset = Convert.ToDateTime(data.data.timings.Sunset).ToString("t");
                data.data.timings.Maghrib = Convert.ToDateTime(data.data.timings.Maghrib).ToString("t");
                data.data.timings.Isha = Convert.ToDateTime(data.data.timings.Isha).ToString("t");
                data.data.timings.Imsak = Convert.ToDateTime(data.data.timings.Imsak).ToString("t");
                data.data.timings.Midnight = Convert.ToDateTime(data.data.timings.Midnight).ToString("t");
            }
            ViewBag.fazar = Convert.ToDateTime(data.data.timings.Fajr).ToString("t");
            ViewBag.sunset = Convert.ToDateTime(data.data.timings.Sunset).ToString("t");

			}
			else
			{
				var ramadanTimeToday = ramadandata.FirstOrDefault(d => d.TheDate.Day == today.Day);

				if (ramadanTimeToday != null)
				{
					ViewBag.fajrTime = ramadanTimeToday.Seheri;
                    ViewBag.maghribTime = ramadanTimeToday.Iftar;
				}
			}
			//return View("ramadanV2");
			return View();
        }

        public async Task<PartialViewResult> GetDivisionWise(string division)
        {
            var ramadan = await ramadanService.RamadanTimeFromALAdhan(division);
            ViewBag.RamadanTime = ramadan;
            return PartialView("ramdan_timeV2Partial");
        }


        public async Task<PartialViewResult> GetRamadanTimingV2()
        {
            var ramadan = await ramadanService.RamadanTimeFromALAdhan("Dahka");
            ViewBag.RamadanTime = ramadan;
            return PartialView("ramdan_timeV2Partial");
        }
    }
}
