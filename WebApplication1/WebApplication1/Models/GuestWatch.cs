using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class GuestWatch
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }
}
