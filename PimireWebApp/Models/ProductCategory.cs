using System;
using System.Collections.Generic;

namespace PimireWebApp.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();

    public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; } = new List<ProductSubCategory>();
}
