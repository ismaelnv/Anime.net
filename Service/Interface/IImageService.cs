
using AnimeWeb.Models;

namespace AnimeWeb.Service.Interface
{
    public interface IImageService
    {

        Task<IEnumerable<ImageModel>> getImages();
        Task<ImageModel?> createImage(ImageModel imageModel);
        Task uploadImage(int imageId, IFormFile image);
        Task<ImageModel?> updateImage(int id, ImageModel image);
        Task<ImageModel?> getImageId(int id);
        Task<ImageModel?> removeImage(int id);
        // Task<GenreModel?> getGenreAnimes(int id);
        // Task<IEnumerable<AnimeModel?>> getAnimesByGenre(string nombreGenero);
        // Task<List<GenreModel>> getGenresId(List<int> genreIds);
    }
}