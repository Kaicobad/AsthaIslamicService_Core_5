using AsthaIslamicService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AsthaIslamicService.Repository.Interfaces
{
    public interface IDuaService
    {
        public Task<List<DuaDetailsModel>> GetAll();
    }
}
