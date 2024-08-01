public enum ImageType
{
    anime = 1,
    chapter = 2
}

public enum ImageCategory
{
    poster = 1,
    cover = 2
}

public class ImageModel
{

    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public DateTime uploadDate { get; set; }
    public DateTime updateDate { get; set; }
    public bool state { get; set; }
    public int? animeId { get; set; }
    public int? chapterId { get; set; }
    public ImageType imageType { get; set; }
    public ImageCategory imageCategory { get; set; }
}