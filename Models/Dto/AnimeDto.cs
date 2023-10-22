
namespace AnimeWeb.Models.Dto
{
    public class AnimeDto
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public bool state { get; set; }
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
    }
}