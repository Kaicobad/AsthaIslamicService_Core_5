using AsthaIslamicService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsthaIslamicService.Repository.Interfaces
{
    public interface IIslamicNameService
    {
        public Task<List<IslamicNameModel>> GetAll(string type);
    }
}
