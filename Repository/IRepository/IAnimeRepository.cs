
using AnimeWeb.Models;
using AnimeWeb.Models.Dto;

namespace AnimeWeb.Repository.IRepository
{
    public interface IAnimeRepository : IRespository<AnimeModel>
    {
        Task<AnimeModel> Update(AnimeModel entidad);
        Task<AnimeModel> getanimeChapters(int id);
    }
}