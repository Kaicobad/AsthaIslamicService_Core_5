namespace AsthaIslamicService.Models
{
    public class Base
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string CategoryName { get; set; }
        public string Subcategory { get; set; }
        public string SubcategoryName { get; set; }

        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ContentType { get; set; }
        public bool IsActive { get; set; }
        public string Language { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public string ContentBaseUrl { get; set; }
        public string ContentUrl { get; set; }
        public int Order { get; set; }
        public bool UserFavouritedThis { get; set; }
    }
}
