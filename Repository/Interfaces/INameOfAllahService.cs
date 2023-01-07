using AsthaIslamicService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsthaIslamicService.Repository.Interfaces
{
    public interface INameOfAllahService
    {
        public Task<List<NameOfAllahViewModel>> GetNames();
    }
}
