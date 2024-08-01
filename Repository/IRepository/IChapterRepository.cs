using AnimeWeb.Models;

namespace AnimeWeb.Repository.IRepository
{
    public interface IChapterRepository : IRespository<ChapterModel>
    {
        
        Task<ChapterModel> UpdateAsync(ChapterModel entidad);
        Task<ChapterModel> getChapterVideosAsync(int id);
        Task<List<ChapterModel>> GetChapters();
    }
}