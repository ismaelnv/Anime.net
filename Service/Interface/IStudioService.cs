using AnimeWeb.Models;
using AnimeWeb.Models.Dto;

namespace AnimeWeb.Service.Interface
{
    public interface IStudioService
    {

        Task<StudioModel> createStudio(CreateStudioDto createStudioDto);
        Task<StudioModel?> getStudioId(int id);
        Task<IEnumerable<StudioDto>> getStudios();
        Task<StudioModel?> removeStudio(int id);
        Task<StudioModel?> updateStudio(int id, UpdateStudioDto updateStudioDto); 
        Task<List<StudioModel>> getStudiosId(List<int> studioIds);
        Task<StudioModel?> getStudioAndAnimes(int id);
    }
}