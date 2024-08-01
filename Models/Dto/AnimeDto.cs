namespace AnimeWeb.Models.Dto
{
    public class AnimeDto : UpdateAnimeDto
    {
        
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public virtual List<ImageModel> Images { get; set; } =  new List<ImageModel>();  
        public virtual List<GenreModel> Genres { get; set; } = new();
    }
}