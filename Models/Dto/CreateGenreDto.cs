
namespace AnimeWeb.Models.Dto
{
    public class CreateGenreDto
    {

        public string name { get; set; } = string.Empty;
        public bool state { get; set; }
        public virtual List<AnimeModel> animes { get; set; } = new();
    }
}