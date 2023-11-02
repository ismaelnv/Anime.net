
namespace AnimeWeb.Models
{
    public class VideoModel
    {
        
        public int id { get; set; }
        public string url { get; set; } = string.Empty;
        public string language { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public int idChapter { get; set; }
        public virtual ChapterModel ChapterModel { get; set; } = null!;
        public bool state { get; set; }
    }
}