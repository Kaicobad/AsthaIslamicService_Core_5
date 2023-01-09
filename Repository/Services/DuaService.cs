using AsthaIslamicService.Hepler;
using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using AsthaIslamicService.Hepler;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Repository.Services
{
    public class DuaService : IDuaService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public DuaService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<List<DuaDetailsModel>> GetAll()
        {
            List<DuaDetailsModel> duaDetailsList = new List<DuaDetailsModel>();
            try
            {
                var client = httpClientFactory.CreateClient("dua");

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("{0}/api/Dua/Dua?msisdn=019&lang=bn", AppConstents.NOOR_API);
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;

                    var data = JsonConvert.DeserializeObject<List<DuaDetailsModel>>(aresult);
                    duaDetailsList = data;
                }
                return duaDetailsList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
