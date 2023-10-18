using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeWeb.Models
{
    [Table ("Capitulo")]
    public class CapituloModel
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public int episode { get; set; }
        public string description { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }

        [ForeignKey("animeID")]
        public int? animeId { get; set; }
        public virtual AnimeModel? animeModel { get; set; }

    }
}