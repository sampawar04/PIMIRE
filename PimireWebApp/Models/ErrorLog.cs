using System;
using System.Collections.Generic;

namespace PimireWebApp.Models;

public partial class ErrorLog
{
    public int Id { get; set; }

    public string? CustomMessage { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime CreatedDate { get; set; }
}
