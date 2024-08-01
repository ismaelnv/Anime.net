namespace AnimeWeb.Models.Dto
{
    public class CAnimeDto
    {
        public CreateAnimeDto createAnimeDto { get; set; } = new CreateAnimeDto();
        public virtual List<int> studioIds { get; set;} = new List<int>();
        public virtual List<int> genreIds { get; set; } = new List<int>();
    }
}