
namespace AnimeWeb.Models.Dto
{
    public class CreateAnimeDto
    {
        
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public bool state { get; set; } 
        public string image { get; set; } = string.Empty;
    }
}