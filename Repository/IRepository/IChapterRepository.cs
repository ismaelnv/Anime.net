
using AnimeWeb.Models;

namespace AnimeWeb.Repository.IRepository
{
    public interface IChapterRepository : IRespository<ChapterModel>
    {
        Task<ChapterModel> Update(ChapterModel entidad);
    }
}