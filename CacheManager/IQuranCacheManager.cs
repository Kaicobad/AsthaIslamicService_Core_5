using AsthaIslamicService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AsthaIslamicService.CacheManager
{
    public interface IQuranCacheManager
    {
        Task<List<SurahModel_Quran>> GetCachedSura();
    }
}
