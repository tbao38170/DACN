using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class SongSinger
{
    public int Id { get; set; }

    public int? IdSong { get; set; }

    public int? IdSinger { get; set; }

    public virtual Singer? IdSingerNavigation { get; set; }

    public virtual Song? IdSongNavigation { get; set; }
}
