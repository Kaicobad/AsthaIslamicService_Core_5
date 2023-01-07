using AsthaIslamicService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsthaIslamicService.CacheManager
{
    public interface IDuaCacheManager
    {
        Task<List<DuaDetailsModel>> GetCachedDua();
    }
}
