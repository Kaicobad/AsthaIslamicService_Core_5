using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AsthaIslamicService.Repository.Services
{
    public class SSOservice : ISSOservice
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SSOservice(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<string> SSO()
        {
            var httpClient = httpClientFactory.CreateClient("deenSSO");

            var requestObject = new
            {
                username = "asthaapp",
                password = "asthagakk",
            };
            string jsonPayload = JsonConvert.SerializeObject(requestObject);
            HttpContent httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://devapi.deenislamic.com/api/User/login", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Response>(responseBody);
                return res.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<SunriseResponse> Sunrise(string token)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("deenSSO");

            string url = "https://devservices.deenislamic.com/api/PrayerTime/TodaysPrayerTime";

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var body = new
            {
                location = "Dhaka",
                language = "bn"
            };

            string jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);
                JToken dataToken = json["Data"];
                string dataJson = dataToken.ToString();

                var res = JsonConvert.DeserializeObject<SunriseResponse>(dataJson);
                return res;
            }
            else
            {
                return null;
            }
        }
    }
}
