using System;
using System.Collections.Generic;

namespace PimireWebApp.Models;

public partial class ProductSubCategoryDetail
{
    public int SubCategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public decimal? Price { get; set; }

    public DateTime? MfgDate { get; set; }

    public virtual ProductSubCategory SubCategory { get; set; } = null!;
}
