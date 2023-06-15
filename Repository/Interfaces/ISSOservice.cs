using AsthaIslamicService.Models;
using System.Threading.Tasks;

namespace AsthaIslamicService.Repository.Interfaces
{
    public interface ISSOservice
    {
        public Task<string> SSO();
        public Task<SunriseResponse> Sunrise(string token);
    }
}
