
namespace AnimeWeb.Models.Dto
{
    public class CAnimeDto
    {
        public CreateAnimeDto createAnimeDto { get; set; }
        public List<int> studioIds { get; set;}
        public List<int> genreIds { get; set; }
    }
}