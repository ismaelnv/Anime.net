
using AnimeWeb.Models;

namespace AnimeWeb.Repository.IRepository
{
    public interface ICapituloRepository : IRespository<CapituloModel>
    {
        Task<CapituloModel> Update(CapituloModel entidad);
    }
}