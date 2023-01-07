using AsthaIslamicService.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsthaIslamicService.Repository.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GetToken();
    }
}
