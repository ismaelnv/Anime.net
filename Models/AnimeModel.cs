using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeWeb.Models
{
    [Table ("Anime")]
    public class AnimeModel
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool status { get; set; }
        public virtual ICollection<CapituloModel> chapters { get; set; } = new List<CapituloModel>(); 

    }
}