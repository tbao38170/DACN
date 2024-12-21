using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class ListOfImageUrl
{
    public int Id { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<Chord> Chords { get; set; } = new List<Chord>();
}
