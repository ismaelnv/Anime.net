namespace AnimeWeb.Models
{
    public class GenreModel
    {

        public int id { get; set; } 
        public string name { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool state { get; set; }
        public virtual List<AnimeModel> animes { get; set; } = new();
    }
}