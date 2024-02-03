namespace PimireWebApp.Models
{
    public class SubCategoryDetailViewModel
    {
        public int CategoryId { get; set; }

        public string ImageUrl { get; set; } = null!;

        public decimal? Price { get; set; }
        public DateTime? MfgDate { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
