namespace AsthaIslamicService.Models
{
    public class DuaModel:Base
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public string TextInArabic { get; set; }
        public string Pronunciation { get; set; }
    }
    public class DuaDetails
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string TextInArabic { get; set; }
        public string Pronunciation { get; set; }
    }
}
