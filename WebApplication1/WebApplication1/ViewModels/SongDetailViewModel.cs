using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class SongDetailViewModel
    {
        public Song Song { get; set; }
        public string AuthorName { get; set; }
        public string SingerName { get; set; }
        public string CategoryName { get; set; }
        public string ChordName { get; set; }
        public List<Lyric> Lyrics { get; set; }
    }
    public class Lyric
    {
        public int Time { get; set; }
        public string Text { get; set; }
    }
}
