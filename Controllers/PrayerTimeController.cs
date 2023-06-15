using AsthaIslamicService.CacheManager;
using AsthaIslamicService.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Threading;
using AsthaIslamicService.Repository.Services;

namespace AsthaIslamicService.Controllers
{
    public class PrayerTimeController : Controller
    {
        private readonly IPrayerTimeCacheManager prayerTimeCacheManager;
        private readonly ISSOservice sSOservice;
        private readonly IPrayerTimeService prayerTimeService;

        public PrayerTimeController(IPrayerTimeCacheManager prayerTimeCacheManager, ISSOservice sSOservice)
        {
            this.prayerTimeCacheManager = prayerTimeCacheManager;
            this.sSOservice = sSOservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetCities()
        {
            var data = await prayerTimeCacheManager.GetCachedCityData();
            return Json(data);
        }
        public async Task<JsonResult> GetDefaultTime()
        {
            var token = await sSOservice.SSO();

            var times = await sSOservice.Sunrise(token);



            var data = await prayerTimeCacheManager.GetCachedData(DateTime.Now.ToString("%d-MMM"));
            data.fazr = Convert.ToDateTime(data.fazr).ToShortTimeString();
            data.juhr = Convert.ToDateTime(data.juhr).ToShortTimeString();
            data.asr = Convert.ToDateTime(data.asr).ToShortTimeString();
            data.magrib = Convert.ToDateTime(data.magrib).ToShortTimeString();
            data.isha = Convert.ToDateTime(data.isha).ToShortTimeString();
            data.Sunrise = times.Sunrise;
            return Json(data);
        }

        public async Task<PartialViewResult> GetPrayerTimeData()
        {
            var prayerTime = await prayerTimeCacheManager.GetCachedData(DateTime.Now.ToString("%d-MMM"));

            var currentTime = DateTime.Now;
            DateTime nextPrayerTime = Convert.ToDateTime(prayerTime.fazr);
            string nextPrayerName = "ফযর";
            string nextPrayerId = "fazr";


            if (currentTime >= Convert.ToDateTime(prayerTime.fazr))
            {
                nextPrayerTime = Convert.ToDateTime(prayerTime.juhr);
                nextPrayerName = "জোহর";
                nextPrayerId = "juhr";
            }
            if (currentTime >= Convert.ToDateTime(prayerTime.juhr))
            {
                nextPrayerTime = Convert.ToDateTime(prayerTime.asr);
                nextPrayerName = "আসর";
                nextPrayerId = "asr";
            }
            if (currentTime >= Convert.ToDateTime(prayerTime.asr))
            {
                nextPrayerTime = Convert.ToDateTime(prayerTime.magrib);
                nextPrayerName = "মাগরিব";
                nextPrayerId = "magrib";
            }
            if (currentTime >= Convert.ToDateTime(prayerTime.magrib))
            {
                nextPrayerTime = Convert.ToDateTime(prayerTime.isha);
                nextPrayerName = "ইশা";
                nextPrayerId = "isha";
            }
            if (currentTime >= Convert.ToDateTime(prayerTime.isha))
            {
                nextPrayerTime = Convert.ToDateTime(prayerTime.fazr);
                nextPrayerName = "ফযর";
                nextPrayerId = "fazr";
            }
            prayerTime.fazr = Convert.ToDateTime(prayerTime.fazr).ToShortTimeString();
            prayerTime.juhr = Convert.ToDateTime(prayerTime.juhr).ToShortTimeString();
            prayerTime.asr = Convert.ToDateTime(prayerTime.asr).ToShortTimeString();
            prayerTime.magrib = Convert.ToDateTime(prayerTime.magrib).ToShortTimeString();
            prayerTime.isha = Convert.ToDateTime(prayerTime.isha).ToShortTimeString();
            ViewBag.prayerTime = prayerTime;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("bn-BD");

            var token = await sSOservice.SSO();

            var times = await sSOservice.Sunrise(token);

            var sunrise = FormatTime(times.Sunrise);
            var sunset = FormatTime(times.Magrib);

            ViewBag.sunriseTime = sunrise;
            ViewBag.sunSetTime = sunset;

            ViewBag.nextPrayerName = nextPrayerName;
            ViewBag.nextPrayerId = nextPrayerId;
            ViewBag.nextPrayerTime = nextPrayerTime.ToShortTimeString();
            ViewBag.currentDate = DateTime.Now.ToLongDateString();

            return PartialView("_partialPrayerTime");
        }
        static string FormatTime(string inputTime)
        {
            CultureInfo cultureInfo = new CultureInfo("bn-BD"); // Use Bengali (Bangladesh) culture
            DateTime time = DateTime.ParseExact(inputTime, "HH:mm:ss", cultureInfo);
            string formattedTime = time.ToString("h:mm tt", cultureInfo);

            return formattedTime;
        }

    }
}
