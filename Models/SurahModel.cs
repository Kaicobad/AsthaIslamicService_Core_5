using System.Collections.Generic;
using System.Security.Cryptography;

namespace AsthaIslamicService.Models
{
    public class SurahModel:Base
    {

        public string NameMeaning { get; set; }
        public string NameInArabic { get; set; }
        public string Origin { get; set; }
        public string ContentUrl { get; set; }
        public string TotalAyat { get; set; }
        public string Duration { get; set; }

        public List<DuaModel> AyatList { get; set; }
    }
}
