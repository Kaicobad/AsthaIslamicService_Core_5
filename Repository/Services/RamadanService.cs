using AsthaIslamicService.Hepler;
using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Globalization;
using System.Security.Cryptography;
using RestSharp;
using System.Text.Json;
using RestSharp.Deserializers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Repository.Services
{
    public class RamadanService : IRamadanService
    {
        private readonly IHttpClientFactory httpClientFactory;
        public RamadanService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<RTDivisionWiseViewModel> GetDivsionWiseTime(string division)
        {
            RTDivisionWiseViewModel aList = new RTDivisionWiseViewModel();
            try
            {
                //"http://27.131.15.12:801/api/prayer/" + city + "/BD"

                var client = httpClientFactory.CreateClient("ramadanTime");

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("http://27.131.15.12:801/api/prayer/{0}/BD", division);
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = await httpResponse.Content.ReadAsStringAsync();

                    aList = JsonConvert.DeserializeObject<RTDivisionWiseViewModel>(aresult);
                    //duaDetailsList = data;
                }
                return aList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Tuple<string, string>> GetGorgeDate(string date)
        {
            string TheDate = "";
            try
            {

                var client = httpClientFactory.CreateClient("IslimicTime");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("http://api.aladhan.com/v1/hToG?date={0}", date);
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    var jsonString = JsonConvert.DeserializeObject<dynamic>(result);
                    var jsParse = jsonString["data"];
                    TheDate = jsParse["gregorian"]["date"];
                    string daysWeek = jsParse["gregorian"]["weekday"]["en"];

                    return Tuple.Create(TheDate, daysWeek);

                }
                return Tuple.Create("", "");
            }
            catch (Exception ex)
            {
                return Tuple.Create("", "");
                throw;
            }
        }

        public async Task<List<RamadanTimeViewModel>> RamadanTimeFromALAdhan(string city)
        {
            string currentMonth = DateTime.Now.Month.ToString();
            string currentYear = DateTime.Now.Year.ToString();

            var client = new RestClient("http://api.aladhan.com/v1/calendarByCity?city=" + city + "&country=Bangladesh&method=9&month=" + currentMonth + "&year=" + currentYear + "&school=1");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            JsonDeserializer deserializer = new JsonDeserializer();
            var jsonString = deserializer.Deserialize<dynamic>(response);
            List<RamadanTimeViewModel> aList = new List<RamadanTimeViewModel>();
            if (jsonString["code"] == 200)
            {
                var jsParse = jsonString["data"];
                foreach (var item in jsParse)
                {
                    RamadanTimeViewModel aDay = new RamadanTimeViewModel();
                    aDay.TheDate = Convert.ToDateTime(item["date"]["readable"]);
                    aDay.DayNumber = item["date"]["hijri"]["day"];

                    IDictionary<string, string> numberNamesEn = new Dictionary<string, string>();
                    numberNamesEn.Add("Fajr", TimeSpanToString12(item["timings"]["Fajr"].Replace("(+06)", "").Trim()));
                    numberNamesEn.Add("Dhuhr", TimeSpanToString12(item["timings"]["Dhuhr"].Replace("(+06)", "").Trim()));
                    numberNamesEn.Add("Asr", TimeSpanToString12(item["timings"]["Asr"].Replace("(+06)", "").Trim()));
                    numberNamesEn.Add("Maghrib", TimeSpanToString12(item["timings"]["Maghrib"].Replace("(+06)", "").Trim()));
                    numberNamesEn.Add("Isha", TimeSpanToString12(item["timings"]["Isha"].Replace("(+06)", "").Trim()));
                    aDay.TimeEN = numberNamesEn;
                    aList.Add(aDay);
                }
            }

            return aList;
        }
        private string TimeSpanToString12(string input)
        {
            var timeFromInput = DateTime.ParseExact(input, "H:m", null, DateTimeStyles.None);
            string timeIn12HourFormatForDisplay = timeFromInput.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            return timeIn12HourFormatForDisplay;
        }
    }
}
