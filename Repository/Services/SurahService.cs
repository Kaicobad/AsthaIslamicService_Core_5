using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net.Http;

namespace AsthaIslamicService.Repository.Services
{
    public class SurahService : ISurahService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SurahService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<List<SurahModel_Quran>> GetAll()
        {
            List<SurahModel_Quran> aList = new List<SurahModel_Quran>();
            try
            {

                var client = httpClientFactory.CreateClient("quarn");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("http://43.240.103.34/ebadattest/api/Quran?lang=bn");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;

                    var data = JsonConvert.DeserializeObject<List<SurahModel_Quran>>(aresult);
                    aList = data;
                }
                return aList;

            }
            catch (Exception ex)
            {
                return aList;
                throw;
            }
        }

        public async Task<List<SurahModel_Quran>> GetById(string id)
        {
            List<SurahModel_Quran> aList = new List<SurahModel_Quran>();

            try
            {
                var client = httpClientFactory.CreateClient("quarn");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("http://43.240.103.34/ebadattest/api/Quran/?Id=" + id + "&lang=bn");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;

                    var data = JsonConvert.DeserializeObject<List<SurahModel_Quran>>(aresult);
                    aList = data;

                }
                return aList;
            }
            catch (Exception)
            {
                return aList;
                throw;
            }
        }
    }
}
