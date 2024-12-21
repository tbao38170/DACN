using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Song
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Content { get; set; }

    public DateTime? DateTime { get; set; }

    public int? View { get; set; }

    public string? Tag { get; set; }

    public string? Link { get; set; }

    public int? IdCountryComposes { get; set; }

    public bool? Activity { get; set; }

    public string? Url { get; set; }

    public string? Lyrics { get; set; }

    public virtual TheCountryThatComposesTheSong? IdCountryComposesNavigation { get; set; }

    public virtual ICollection<SongAuthor> SongAuthors { get; set; } = new List<SongAuthor>();

    public virtual ICollection<SongCategory> SongCategories { get; set; } = new List<SongCategory>();

    public virtual ICollection<SongChord> SongChords { get; set; } = new List<SongChord>();

    public virtual ICollection<SongSinger> SongSingers { get; set; } = new List<SongSinger>();

    public virtual ICollection<SongUser> SongUsers { get; set; } = new List<SongUser>();
}
