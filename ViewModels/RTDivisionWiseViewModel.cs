using System.Collections.Generic;

namespace AsthaIslamicService.ViewModels
{
    public class RTDivisionWiseViewModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public string totalRecords { get; set; }
        public string totalPage { get; set; }
        public List<RamadanTimingViewModel> data { get; set; }
    }
    public class RamadanTimingViewModel
    {

        public string ramadaDate { get; set; }
        public string ramadaDay { get; set; }
        public string sehri { get; set; }
        public string iftar { get; set; }
        public string dayValue { get; set; }
    }
}
