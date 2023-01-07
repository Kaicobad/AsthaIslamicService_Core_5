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
    public class HadithService : IHadithService
    {
        private readonly ITokenService tokenService;
        private readonly IHttpClientFactory httpClientFactory;

        public HadithService(ITokenService tokenService,IHttpClientFactory httpClientFactory)
        {
            this.tokenService = tokenService;
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<List<HadithViewModel>> GetAll()
        {
            var textContents = new ResponseData<List<HadithViewModel>>();
            try
            {
                var token = await tokenService.GetToken();

                var client = httpClientFactory.CreateClient("Hadith");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var url = string.Format("{0}/api/textcontent/bycategory/{1}/undefined/1/100",
                           AppConstents.NOOR_API, "601f8eced3753b861d75d8e7");

                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    textContents = JsonConvert.DeserializeObject<ResponseData<List<HadithViewModel>>>(result);
                }

                return textContents.data;
            }
            catch (Exception ex)
            {
                return textContents.data;
                throw;
            }
        }
    }
}
