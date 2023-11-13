
namespace AnimeWeb.Models
{
    public class AnimeModel
    {
      
      public int Id { get; set; }
      public string name { get; set; } = string.Empty;
      public string description { get; set; } = string.Empty;
      public DateTime uploadDate { get; set; }
      public DateTime updateDate { get; set; }
      public string image { get; set; } = string.Empty;
      public bool state { get; set; }
      public virtual ICollection<ChapterModel> chapters { get; } = new List<ChapterModel>(); 
      public virtual List<GenreModel> Genres { get; set; } = new();
      public virtual List<StudioModel> Studios { get; set; } = new List<StudioModel>();    
    }
}