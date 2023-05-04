using AsthaIslamicService.Hepler;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Repository.Services
{
    public class PrayerTimeService : IPrayerTimeService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public PrayerTimeService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<List<BDPlacesViewModel>> GetBDCities()
        {
            ResponseData<List<BDPlacesViewModel>>? placeList = new ResponseData<List<BDPlacesViewModel>>();
            try
            {
                var client = httpClientFactory.CreateClient("getSalahTime");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = string.Format("{0}/api/prayertime/GetPrayerTimeCity",
                              AppConstents.NOOR_API);
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    placeList = JsonConvert.DeserializeObject<ResponseData<List<BDPlacesViewModel>>>(result);
                }

                return placeList.data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PrayerTimeViewModel> GetPrayerTime(string date)
        {
            var timeList = new ResponseData<List<PrayerTimeViewModel>>();
            try
            {
                var client = httpClientFactory.CreateClient("getSalahTime");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = string.Format("{0}/api/prayertime/GetPrayerTime/{1}",
                              AppConstents.NOOR_API, date);
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    timeList = JsonConvert.DeserializeObject<ResponseData<List<PrayerTimeViewModel>>>(result);
                }

                return timeList.data[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<PrayerTime> GetPrayerTime()
        {
            string latitude = "23.7808875";
            string longitude = "90.2792366";
            PrayerTime aPrayerTime = new PrayerTime();
            var client = httpClientFactory.CreateClient("prayertime");

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var url = string.Format("https://api.aladhan.com/v1/timings/{0}?latitude={1}&longitude={2}&method=2", DateTime.Now.ToString("dd-MM-yyyy"), latitude, longitude);
            var httpResponse = await client.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                var aresult = await httpResponse.Content.ReadAsStringAsync();

                aPrayerTime = JsonConvert.DeserializeObject<PrayerTime>(aresult);
                //duaDetailsList = data;
            }
            return aPrayerTime;
        }
    }
}
