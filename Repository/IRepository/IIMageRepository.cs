using AnimeWeb.Models;

namespace AnimeWeb.Repository.IRepository
{
    public interface IImageRepository : IRespository <ImageModel>
    {
        
        Task<ImageModel> UpdateAsync(ImageModel image);
       // Task<ImageModel> getGenreAnimesAsync(int id);
       // Task<List<ImageModel>> GetAnimesByGenreAsync(string );
       // Task<List<ImageModel>> GetByIdsAsync(List<int> genreIds);  
    }
}