using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class SongAuthor
{
    public int Id { get; set; }

    public int? IdSong { get; set; }

    public int? IdAuthor { get; set; }

    public virtual Author? IdAuthorNavigation { get; set; }

    public virtual Song? IdSongNavigation { get; set; }
}
