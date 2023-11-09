using AnimeWeb.Models;
using AnimeWeb.Models.Dto;

namespace AnimeWeb.Service.Interface
{
    public interface IChapterService
    {
        Task<IEnumerable<ChapterDto>> getChapters();
        Task<ChapterModel?> getChapterId(int id);
        Task<ChapterModel?> createChapter(CreateChapterDto createChapterDto);
        Task<ChapterModel?> removeChapter(int id);
        Task<ChapterModel?> updateChapter(int id, UpdateChapterDto updateChapterDto);
        Task<ChapterModel?> getChapterVideos(int id);
        Task<IEnumerable<ChapterDto>> orderTheChaptersFromSmallestToLargest();
    }
}