using AnimeWeb.Models;

namespace AnimeWeb.Repository.IRepository
{
    public interface IVideoRepository : IRespository<VideoModel>
    {
        
        Task<VideoModel> UpdateAsync(VideoModel entidad);
    }
}