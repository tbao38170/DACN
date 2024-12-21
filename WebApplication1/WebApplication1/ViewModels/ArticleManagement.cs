using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class ArticleManagement
	{
        public List<Song> Songs { get; set; }
        public List<Author> Authors { get; set; }
		public List<SongAuthor> SongAuthors { get; set; }
		public List<SongSinger> SongSingers { get; set; }
		public List<Singer> Singers { get; set; }
		public List<Category> Categories { get; set; }
		public List<SongCategory> SongCategories { get; set; }
		public List<Chord> Chords { get; set; }
		public List<SongChord> SongChords { get; set; }	
	}
}
