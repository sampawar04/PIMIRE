using System;
using System.Collections.Generic;

namespace PimireWebApp.Models;

public partial class Enquiry
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string CustomerMobile { get; set; } = null!;

    public string? CustomerAddress { get; set; }

    public string? Comments { get; set; }

    public bool IsEmailSent { get; set; }

    public bool IsEmailReceived { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ProductCategory Category { get; set; } = null!;

    public virtual ProductSubCategory SubCategory { get; set; } = null!;
}
