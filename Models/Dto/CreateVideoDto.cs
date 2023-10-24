
namespace AnimeWeb.Models.Dto
{
    public class CreateVideoDto
    {
        public string url { get; set; } = string.Empty;
        public int idChapter { get; set; }
        public bool state { get; set; }
    }
}