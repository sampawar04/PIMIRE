using System;
using System.Collections.Generic;

namespace PimireWebApp.Models;

public partial class User
{
    public int Id { get; set; }

    public int RoleTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string? Address { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual RoleType RoleType { get; set; } = null!;
}
