namespace AnimeWeb.Service.Interface
{
    public interface IImageService
    {

        Task<IEnumerable<ImageModel>> getImages();
        Task<ImageModel?> createImage(ImageModel imageModel);
        Task uploadImage(int imageId, IFormFile image);
        Task<ImageModel?> getImageId(int id);
        Task<ImageModel?> removeImage(int id);
    }
}