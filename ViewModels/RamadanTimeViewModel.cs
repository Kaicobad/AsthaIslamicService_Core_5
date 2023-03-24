using System;
using System.Collections.Generic;

namespace AsthaIslamicService.ViewModels
{
    public class RamadanTimeViewModel
    {
        public IDictionary<string, string> TimeEN { get; set; }
        public IDictionary<string, string> TimeBng { get; set; }
        public string Iftar { get; set; }
        public string Seheri { get; set; }
        public DateTime TheDate { get; set; }
        public string DayNumber { get; set; }
    }
}
