using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int? IdUserGroup { get; set; }

    public virtual UserGroup? IdUserGroupNavigation { get; set; }

    public virtual ICollection<SongUser> SongUsers { get; set; } = new List<SongUser>();
}
