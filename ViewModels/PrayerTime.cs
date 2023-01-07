namespace AsthaIslamicService.ViewModels
{
    public class PrayerTime
    {
        public string code { get; set; }
        public string status { get; set; }
        public Timings data { get; set; }
    }
    public class Timings
    {
        public timings timings { get; set; }
    }
    public class timings
    {
        public string Fajr { get; set; }
        public string Dhuhr { get; set; }
        public string Asr { get; set; }
        public string Sunset { get; set; }
        public string Maghrib { get; set; }
        public string Isha { get; set; }
        public string Imsak { get; set; }
        public string Midnight { get; set; }
    }
}
