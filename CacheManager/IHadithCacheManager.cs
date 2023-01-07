using AsthaIslamicService.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsthaIslamicService.CacheManager
{
    public interface IHadithCacheManager
    {
        Task<List<HadithViewModel>> GetCachedHadith();
    }
}
