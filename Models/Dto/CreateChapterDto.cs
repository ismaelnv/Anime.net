
namespace AnimeWeb.Models.Dto
{
    public class CreateChapterDto
    {
        
        public string title { get; set; } = string.Empty;
        public int episode { get; set; }
        public string description { get; set; } = string.Empty;
        public int animeId { get; set; }
        public bool state { get; set; }
    }
}