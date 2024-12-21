using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class SingerTone
{
    public int Id { get; set; }

    public int? IdSinger { get; set; }

    public int? IdTone { get; set; }

    public virtual Singer? IdSingerNavigation { get; set; }

    public virtual Tone? IdToneNavigation { get; set; }
}
