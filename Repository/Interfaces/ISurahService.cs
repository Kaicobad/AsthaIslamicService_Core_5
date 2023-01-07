using AsthaIslamicService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsthaIslamicService.Repository.Interfaces
{
    public interface ISurahService
    {
        public Task<List<SurahModel_Quran>> GetAll();
        public Task<List<SurahModel_Quran>> GetById(string id);
    }
}
