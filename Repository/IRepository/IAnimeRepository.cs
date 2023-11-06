
using AnimeWeb.Models;
using AnimeWeb.Models.Dto;

namespace AnimeWeb.Repository.IRepository
{
    public interface IAnimeRepository : IRespository<AnimeModel>
    {
        
        Task<AnimeModel> UpdateAsync(AnimeModel entidad);
        Task<AnimeModel> GetanimeChaptersAsync(int id);
        Task<List<AnimeDto>> GetAllAnimeDtoAsync();
        Task<AnimeModel> GetAnimeAndGenres(int id);
    }
}