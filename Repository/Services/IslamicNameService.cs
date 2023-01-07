using AsthaIslamicService.Hepler;
using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AsthaIslamicService.Repository.Services
{
    public class IslamicNameService : IIslamicNameService
    {
        private readonly ITokenService tokenService;
        private readonly IHttpClientFactory httpClientFactory;

        public IslamicNameService(ITokenService tokenService, IHttpClientFactory httpClientFactory)
        {
            this.tokenService = tokenService;
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<List<IslamicNameModel>> GetAll(string type)
        {
            ResponseData<List<IslamicNameModel>> textContents = new ResponseData<List<IslamicNameModel>>();
            try
            {
                var token = await tokenService.GetToken();

                var client = httpClientFactory.CreateClient("wallpaper");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var url = string.Format("{0}/api/islamicname/bn/null/null/{1}",
                           AppConstents.NOOR_API, type);

                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();
                    textContents = JsonConvert.DeserializeObject<ResponseData<List<IslamicNameModel>>>(result);
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
