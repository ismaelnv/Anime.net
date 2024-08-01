using AnimeWeb.Models;

namespace AnimeWeb.Repository.IRepository
{
    public interface IStudioRepository : IRespository<StudioModel>
    {

        Task<StudioModel> UpdateAsync(StudioModel entidad);
        Task<List<StudioModel>> GetByIdsAsync(List<int> studioIds);    
        Task<StudioModel> GetStudioAndAnimes(int id);
    }    
}