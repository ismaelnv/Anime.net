using AnimeWeb.Models;
using AnimeWeb.Models.Dto;

namespace AnimeWeb.Service.Interface
{
    public interface IVideoService
    {
        Task<IEnumerable<VideoDto>> getVideos();
        Task<VideoModel?> getVideoId(int id);
        Task<VideoModel?> CreateVideo(CreateVideoDto createVideoDto);
        Task<VideoModel?> removeVideo(int id);
        Task<VideoModel?> updateVideo(int id, UpdateVideoDto updateVideoDto);  
    }
}