
namespace AnimeWeb.Models.Dto
{
    public class CAnimeDto
    {
        public CreateAnimeDto createAnimeDto { get; set; }
        public virtual List<int> studioIds { get; set;}
        public virtual List<int> genreIds { get; set; }
    }
}