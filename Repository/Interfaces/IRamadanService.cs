using AsthaIslamicService.Models;
using AsthaIslamicService.ViewModels;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AsthaIslamicService.Repository.Interfaces
{
    public interface IRamadanService
    {
        public Task<RTDivisionWiseViewModel> GetDivsionWiseTime(string division);
        public Task<Tuple<string, string>> GetGorgeDate(string date);
        public Task<List<RamadanTimeViewModel>> RamadanTimeFromALAdhan(string city);
    }
}
