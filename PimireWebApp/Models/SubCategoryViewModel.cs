namespace PimireWebApp.Models
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string Title { get; set; } = null!;
        public string ImageURL { get; set; } 
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public SubCategoryDetailViewModel SubCategoryDetailViewModel { get; set; }
    }
}
