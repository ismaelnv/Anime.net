using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using AnimeWeb.Service.Interface;
using AutoMapper;

namespace AnimeWeb.Service
{
    public class ChapterService : IChapterService
    {

        private IChapterRepository _chapterRepository;
        private IAnimeService _animeService;
        private IMapper _mapper;

        public ChapterService(IChapterRepository chapterRepository, IMapper mapper, IAnimeService animeService)
        {

            _chapterRepository = chapterRepository;
            _mapper = mapper;
            _animeService = animeService;
        }

        public async Task<IEnumerable<ChapterDto>> getChapters()
        {

            IEnumerable<ChapterModel> chapterModeList = await _chapterRepository.GetAllAsync();
            IEnumerable<ChapterDto> chapterDtoList = _mapper.Map<IEnumerable<ChapterDto>>(chapterModeList);
            return chapterDtoList;
        }

        public async Task<ChapterModel?> getChapterId(int id)
        {

            if (id == 0)
            {

                throw new BadHttpRequestException("Invalid Id");
            }

            ChapterModel chapter = await _chapterRepository.ObtainAsync(C => C.id == id);

            if (chapter == null)
            {
                return null;
            }

            return chapter;
        }

        public async Task<ChapterModel?> createChapter(CreateChapterDto createChapterDto)
        {

            if (createChapterDto == null)
            {
                throw new BadHttpRequestException("Invalid chapter");
            }

            AnimeModel? animeModel = await _animeService.getAnimeId(createChapterDto.animeId);

            if (animeModel == null)
            {
                return null;
            }

            ChapterModel chapter = _mapper.Map<ChapterModel>(createChapterDto);
            chapter.AnimeModel = animeModel;
            chapter.uploadDate = DateTime.Now;
            chapter.updateDate = DateTime.Now;

            await _chapterRepository.CreateAsync(chapter);
            return chapter;
        }

        public async Task<ChapterModel?> removeChapter(int id)
        {

            if (id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            ChapterModel? chapterModel = await this.getChapterId(id);

            if (chapterModel == null)
            {
                return null;
            }

            await _chapterRepository.RemoveAsync(chapterModel);

            return chapterModel;
        }

        public async Task<ChapterModel?> updateChapter(int id, UpdateChapterDto updateChapterDto)
        {

            if (updateChapterDto.id != id)
            {
                throw new BadHttpRequestException("The id you want to update must match the chapter id");
            }

            if (updateChapterDto == null)
            {
                throw new BadHttpRequestException("Invalid chapter");
            }

            ChapterModel chapterModel = _mapper.Map<ChapterModel>(updateChapterDto);
            ChapterModel chapter = await _chapterRepository.UpdateAsync(chapterModel);

            if (chapter == null)
            {
                return null;
            }

            return chapter;
        }

        public async Task<ChapterModel?> getChapterVideos(int id)
        {

            if (id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            ChapterModel? chapter = await _chapterRepository.getChapterVideosAsync(id);

            if (chapter == null)
            {
                return null;
            }

            return chapter;
        }

        public async Task<IEnumerable<ChapterDto>> orderTheChaptersFromSmallestToLargest()
        {
            
            IEnumerable<ChapterDto> chapters =  await this.getChapters();
            IEnumerable<ChapterDto> orderedChapters = chapters.OrderBy(chapter => chapter.episode);

            return orderedChapters;
        }
    }
}