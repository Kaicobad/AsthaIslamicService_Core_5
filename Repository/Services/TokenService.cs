using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Repository.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public TokenService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetToken()
        {
            LoginResponse response = new LoginResponse();
            try
            {
                LoginViewModel loginModel = new LoginViewModel
                {
                    Msisdn = "8801819509506",
                    Password = "123456",
                    UserName = ""
                };

                var loginObj = new
                {
                    UserName = loginModel.UserName,
                    Password = loginModel.Password,
                    MobileNumber = loginModel.Msisdn
                };

                var url = "http://118.67.219.130:801/api/account/loginmu";

                string jsonString = JsonConvert.SerializeObject(loginObj);
                var formContent = new FormUrlEncodedContent(new[]
                          {
                        new KeyValuePair<string, string>("payload", jsonString),

                    });

                var client = httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage responseHttp = await client.PostAsync(url, formContent);
                string responseJson = await responseHttp.Content.ReadAsStringAsync();
                if (responseHttp.IsSuccessStatusCode)
                {
                    var aresult = await responseHttp.Content.ReadAsStringAsync();

                    response = JsonConvert.DeserializeObject<LoginResponse>(aresult);

                    return response.data.token;
                }
                else
                {
                    return "Something Went Wrong !";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //return response.data.token;
        }
    }
}
