using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class GroupOfSinger
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? NumberOfMembers { get; set; }

    public virtual ICollection<SingerGroup> SingerGroups { get; set; } = new List<SingerGroup>();
}
