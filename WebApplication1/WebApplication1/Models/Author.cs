using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Author
{
    public int Id { get; set; }

    public string? ComposerName { get; set; }

    public string? AuthorName { get; set; }

    public virtual ICollection<SongAuthor> SongAuthors { get; set; } = new List<SongAuthor>();
}
