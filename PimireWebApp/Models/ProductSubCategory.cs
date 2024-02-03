using System;
using System.Collections.Generic;

namespace PimireWebApp.Models;

public partial class ProductSubCategory
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ProductCategory Category { get; set; } = null!;

    public virtual ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();

    public virtual ProductSubCategoryDetail? ProductSubCategoryDetail { get; set; }
}
