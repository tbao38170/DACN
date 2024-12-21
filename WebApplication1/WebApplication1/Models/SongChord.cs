using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class SongChord
{
    public int Id { get; set; }

    public int? IdSong { get; set; }

    public int? IdChord { get; set; }

    public virtual Chord? IdChordNavigation { get; set; }

    public virtual Song? IdSongNavigation { get; set; }
}
