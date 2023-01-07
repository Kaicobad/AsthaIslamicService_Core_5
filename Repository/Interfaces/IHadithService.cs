using AsthaIslamicService.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace AsthaIslamicService.Repository.Interfaces
{
    public interface IHadithService
    {
        public Task<List<HadithViewModel>> GetAll();
    }
}
