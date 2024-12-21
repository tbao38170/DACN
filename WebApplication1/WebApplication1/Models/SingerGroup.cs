using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class SingerGroup
{
    public int Id { get; set; }

    public int? IdSinger { get; set; }

    public int? IdGroupOfSinger { get; set; }

    public virtual GroupOfSinger? IdGroupOfSingerNavigation { get; set; }

    public virtual Singer? IdSingerNavigation { get; set; }
}
