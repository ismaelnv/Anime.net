
namespace AnimeWeb.Models.Dto
{
    public class UpdateAnimeDto
    {

      public int Id { get; set; } 
      public string name { get; set; } = string.Empty;
      public string description { get; set; } = string.Empty;
      public bool state { get; set; }
    }
}