using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Tone
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdChord { get; set; }

    public virtual Chord? IdChordNavigation { get; set; }

    public virtual ICollection<SingerTone> SingerTones { get; set; } = new List<SingerTone>();
}
