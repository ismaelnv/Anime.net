
using AnimeWeb.Models;

namespace AnimeWeb.Repository.IRepository
{
    public interface IGenreRepository : IRespository <GenreModel>
    {
        
        Task<GenreModel> UpdateAsync(GenreModel entidad);
        Task<GenreModel> getGenreAnimesAsync(int id);
        Task<List<AnimeModel>> GetAnimesByGenreAsync(string nameGenre);
        Task<List<GenreModel>> GetByIdsAsync(List<int> genreIds);  
    }
}