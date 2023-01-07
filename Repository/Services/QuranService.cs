using AsthaIslamicService.Hepler;
using AsthaIslamicService.Models;
using AsthaIslamicService.Repository.Interfaces;
using AsthaIslamicService.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Repository.Services
{
    public class QuranService : IQuranService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ITokenService tokenService;

        public QuranService(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            this.httpClientFactory = httpClientFactory;
            this.tokenService = tokenService;
        }
        //public async Task<List<DuaModel>> GetAyatBySuraId(string id)
        //{
        //    var token = await tokenService.GetToken();
        //    var ayatList = new ResponseData<List<DuaModel>>();
        //    var client = httpClientFactory.CreateClient("getsurah-byid");
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var url = string.Format("{0}/api/ayat/bysurah/{1}/1/5000",
        //                  AppConstents.NOOR_API, id);
        //    var httpResponse = await client.GetAsync(url);
        //    if (httpResponse.IsSuccessStatusCode)
        //    {
        //        var result = await httpResponse.Content.ReadAsStringAsync();
        //        ayatList = JsonConvert.DeserializeObject<ResponseData<List<DuaModel>>>(result);
        //    }
        //    return ayatList.data;
        //}

        //public async Task<List<SurahModel>> GetSurah()
        //{
        //    var token = await tokenService.GetToken();
        //    var surahList = new ResponseData<List<SurahModel>>();
        //    var client = httpClientFactory.CreateClient("getsurah");
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var url = string.Format("{0}/api/surah/bn/{1}/{2}",
        //                  AppConstents.NOOR_API, 1, 500);
        //    var httpResponse = await client.GetAsync(url);
        //    if (httpResponse.IsSuccessStatusCode)
        //    {
        //        var result = await httpResponse.Content.ReadAsStringAsync();
        //        surahList = JsonConvert.DeserializeObject<ResponseData<List<SurahModel>>>(result);
        //    }

        //    return surahList.data;
        //}
        //public async Task<SurahModel> GetSurah(string id)
        //{
        //    var token = await tokenService.GetToken();
        //    var aSurah = new ResponseData<SurahModel>();
        //    var client = httpClientFactory.CreateClient("getsurah-byid");
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var url = string.Format("{0}/api/surah/{1}",
        //                  AppConstents.NOOR_API, id);
        //    var httpResponse = await client.GetAsync(url);
        //    if (httpResponse.IsSuccessStatusCode)
        //    {
        //        var result = await httpResponse.Content.ReadAsStringAsync();
        //        aSurah = JsonConvert.DeserializeObject<ResponseData<SurahModel>>(result);
        //        aSurah.data.AyatList = await GetAyatBySuraId(id);
        //    }

        //    return aSurah.data;
        //}
    }
}
