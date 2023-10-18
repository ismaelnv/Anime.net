
using AnimeWeb.Models;

namespace AnimeWeb.Repository.IRepository
{
    public interface IAnimeRepository : IRespository<AnimeModel>
    {
        Task<AnimeModel> Update(AnimeModel entidad);
    }
}