
namespace AnimeWeb.Models.Dto
{
    public class AnimeDto : UpdateAnimeDto
    {
        
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
    
    }
}