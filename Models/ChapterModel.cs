
namespace AnimeWeb.Models
{
    public class ChapterModel
    {
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public int episode { get; set; }
        public string description { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public int animeId { get; set; }
        public virtual AnimeModel AnimeModel { get; set; } = null!;
        public bool state { get; set; }
        public virtual ICollection<VideoModel> videos { get; } = new List<VideoModel>(); 
    }
}