
namespace AnimeWeb.Models.Dto
{
    public class UpdateVideoDto
    {
        
        public int id { get; set; }
        public string url { get; set; } = string.Empty;
        public int idChapter { get; set; }
        public bool state { get; set; }
    }
}