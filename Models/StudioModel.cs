namespace AnimeWeb.Models
{
    public class StudioModel
    {

        public  int id { get; set; }
        public string name { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool state { get; set; }   
        public virtual IEnumerable<AnimeModel> Animes { get; set; } = new List<AnimeModel>();    
    }
}