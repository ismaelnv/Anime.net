
namespace AnimeWeb.Models.Dto
{
    public class VideoDto
    {
        
        public int id { get; set; }
        public string url { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public int idChapter { get; set; }
        public bool state { get; set; }
    }
}