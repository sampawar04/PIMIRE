using System;
using System.Collections.Generic;

namespace PimireWebApp.Models;

public partial class RoleType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
