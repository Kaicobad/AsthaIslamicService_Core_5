using AsthaIslamicService.Hepler;
using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using AsthaIslamicService.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Repository.Services
{
    public class NameOfAllahService : INameOfAllahService
    {
        private readonly ITokenService tokenService;
        private readonly IHttpClientFactory httpClientFactory;

        public NameOfAllahService(ITokenService tokenService,IHttpClientFactory httpClientFactory)
        {
            this.tokenService = tokenService;
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<List<NameOfAllahViewModel>> GetNames()
        {
            var textContents = new ResponseData<List<NameOfAllahViewModel>>();
            try
            {
                var token = await tokenService.GetToken();

                var client = httpClientFactory.CreateClient("name_service");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var url = string.Format("{0}/api/ninetyninename/bn/1/100",
                              AppConstents.NOOR_API);

                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    textContents = JsonConvert.DeserializeObject<ResponseData<List<NameOfAllahViewModel>>>(result);
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
