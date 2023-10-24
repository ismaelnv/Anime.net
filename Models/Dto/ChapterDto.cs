
namespace AnimeWeb.Models.Dto
{
    public class ChapterDto
    {
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public int episode { get; set; }
        public string description { get; set; } = string.Empty;
        public bool state { get; set; }
        public int animeId { get; set; }
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }

    }
}