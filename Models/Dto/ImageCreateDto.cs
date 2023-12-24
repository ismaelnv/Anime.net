
namespace AnimeWeb.Models;


public class ImageCreateDto
{
    public string name { get; set; } = string.Empty;
    public DateTime uploadDate { get; set; }
    public DateTime updateDate { get; set; }
    public bool state { get; set; }
    public  int? animeId { get; set; }
    public  int? chapterId { get; set; }
    public ImageType imageType { get; set; }
    public ImageCategory imageCategory { get; set; }
}