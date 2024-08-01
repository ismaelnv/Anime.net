namespace AnimeWeb.Models.Dto
{
    public class VideoDto : UpdateVideoDto
    {
        
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
    }
}