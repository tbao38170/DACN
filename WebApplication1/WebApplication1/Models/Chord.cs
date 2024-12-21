using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Chord
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdChordGroup { get; set; }

    public int? IdChordType { get; set; }

    public int? IdListOfImageUrls { get; set; }

    public bool? KeyNo { get; set; }

    public virtual ChordGroup? IdChordGroupNavigation { get; set; }

    public virtual ChordType? IdChordTypeNavigation { get; set; }

    public virtual ListOfImageUrl? IdListOfImageUrlsNavigation { get; set; }

    public virtual ICollection<SongChord> SongChords { get; set; } = new List<SongChord>();

    public virtual ICollection<Tone> Tones { get; set; } = new List<Tone>();
}
