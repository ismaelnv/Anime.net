using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;

namespace AnimeWeb.Service
{
    public class ChapterService
    {
        private readonly IChapterRepository _chapterRepository;

        public ChapterService(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        public async Task<IEnumerable<ChapterModel>> getChapters()
        {
            return await _chapterRepository.GetAll();
        }

        public async Task<ChapterModel> getChapterId(int id)
        {
            if(id == 0){
                return null;
            }

            var capitulo = await _chapterRepository.Obtain(C => C.id == id);
            return capitulo;
        }

        public async Task<ChapterModel> createChapter(ChapterModel chapterModel)
        {
            if(chapterModel == null)
            {
                return null;
            }

            await _chapterRepository.Create(chapterModel);
            return chapterModel;
        } 

        public async Task<ChapterModel> removeChapter(int id)
        {
            var capitulo = await this.getChapterId(id);
            await _chapterRepository.Remove(capitulo);
            return capitulo;
        }

        public async Task<ChapterModel> updateChapter(int id, ChapterModel chapterModel)
        {
            if (chapterModel.id != id || chapterModel == null)
            {
                return null;
            }

            var capitulo = await _chapterRepository.Update(chapterModel);
            return capitulo;
        }

    }
}