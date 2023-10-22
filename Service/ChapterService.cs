using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;


namespace AnimeWeb.Service
{
    public class ChapterService
    {
        private readonly IChapterRepository _chapterRepository;
        private readonly IAnimeRepository _animeRepository;

        public ChapterService(IChapterRepository chapterRepository,IAnimeRepository animeRepository)
        {
            _chapterRepository = chapterRepository;
            _animeRepository = animeRepository;
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

            ChapterModel capitulo = await _chapterRepository.Obtain(C => C.id == id);
            return capitulo;
        }

        public async Task<CreateChapterDto> createChapter(CreateChapterDto chapterDto)
        {
            if(chapterDto == null)
            {
                return null;
            }

            ChapterModel chapter = new()
            {
                title = chapterDto.title,
                episode = chapterDto.episode,
                description = chapterDto.description,
                animeId = chapterDto.animeId,
                state = chapterDto.state,
                AnimeModel = await _animeRepository.Obtain(A => A.Id == chapterDto.animeId)
            };

            await _chapterRepository.Create(chapter);
            return chapterDto;
        } 

        public async Task<ChapterModel> removeChapter(int id)
        {
            ChapterModel capitulo = await this.getChapterId(id);
            await _chapterRepository.Remove(capitulo);
            return capitulo;
        }

        public async Task<UpdateChapterDto> updateChapter(int id, UpdateChapterDto chapterDto)
        {
            if (chapterDto.id != id || chapterDto == null)
            {
                return null;
            }

            ChapterModel chapter = new()
            {
                id = chapterDto.id,
                title = chapterDto.title,
                episode = chapterDto.episode,
                description = chapterDto.description,
                animeId = chapterDto.animeId,
                state = chapterDto.state
            };

            await _chapterRepository.Update(chapter);
            return chapterDto;
        }

    }
}