using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Singer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Content { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<SingerGroup> SingerGroups { get; set; } = new List<SingerGroup>();

    public virtual ICollection<SingerTone> SingerTones { get; set; } = new List<SingerTone>();

    public virtual ICollection<SongSinger> SongSingers { get; set; } = new List<SongSinger>();
}
