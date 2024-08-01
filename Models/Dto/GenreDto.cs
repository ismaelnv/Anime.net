namespace AnimeWeb.Models.Dto
{
    public class GenreDto : updateGenreDto
    {

        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public virtual List<AnimeModel> Animes { get; set; } = new();
    }
}