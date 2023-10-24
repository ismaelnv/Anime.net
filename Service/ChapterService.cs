using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using AutoMapper;

namespace AnimeWeb.Service
{
    public class ChapterService
    {
        private IChapterRepository _chapterRepository;
        private IAnimeRepository _animeRepository;
        private IVideoRepository _videoRepository;
        private AnimeService _animeService;
        private IMapper _mapper;

        public ChapterService(IChapterRepository chapterRepository,IAnimeRepository animeRepository,
            IVideoRepository videoRepository, IMapper mapper, AnimeService animeService)
        {
            _chapterRepository = chapterRepository;
            _animeRepository = animeRepository;
            _videoRepository = videoRepository;
            _mapper = mapper;
            _animeService = animeService;
        }

        public async Task<IEnumerable<ChapterDto>> getChapters()
        {
            IEnumerable<ChapterModel> chapterModeList = await _chapterRepository.GetAllAsync();
            IEnumerable<ChapterDto> chapterDtoList = _mapper.Map<IEnumerable<ChapterDto>>(chapterModeList);
            return chapterDtoList;
        }

        public async Task<ChapterDto> getChapterId(int id)
        {
            if(id == 0){
                return null;
            }

            ChapterModel chapter = await _chapterRepository.ObtainAsync(C => C.id == id);

            if (chapter == null)
            {
                return null;
            }

            ChapterDto chapterDto = _mapper.Map<ChapterDto>(chapter);
            return chapterDto;
        }

        public async Task<ChapterDto> createChapter(CreateChapterDto createChapterDto)
        {
            if(createChapterDto == null)
            {
                return null;
            }

            ChapterModel chapter = _mapper.Map<ChapterModel>(createChapterDto);
            chapter.AnimeModel = await _animeRepository.ObtainAsync(a => a.Id == chapter.animeId);

            if (chapter.AnimeModel == null)
            {
                return null;
            }

            await _chapterRepository.CreateAsync(chapter);

            ChapterDto chapterDto = _mapper.Map<ChapterDto>(chapter);

            return chapterDto;
        } 

        public async Task<ChapterDto> removeChapter(int id)
        {
            if (id == 0)
            {
                return null;
            }

            ChapterModel chapterModel = await _chapterRepository.ObtainAsync(c => c.id == id);
            Console.WriteLine(chapterModel);

            if (chapterModel == null)
            {
                return null;
            }

            await _chapterRepository.RemoveAsync(chapterModel);
          
            ChapterDto chapterDto = _mapper.Map<ChapterDto>(chapterModel);
            return chapterDto;
        }

        public async Task<ChapterDto> updateChapter(int id, UpdateChapterDto updateChapterDto)
        {
            if (updateChapterDto.id != id || updateChapterDto == null)
            {
                return null;
            }

            ChapterModel chapterModel = _mapper.Map<ChapterModel>(updateChapterDto);
          
            ChapterModel chapter = await _chapterRepository.UpdateAsync(chapterModel);
            
            if (chapter == null)
            {
                return null;
            }

            ChapterDto chapterDto = _mapper.Map<ChapterDto>(chapter);
            return chapterDto;
        }

        public async Task<ChapterModel> getChapterCapitulos(int id)
        {
            if ( id == 0)
            {
                return null;
            }

            ChapterModel chapter = await _chapterRepository.getChapterVideosAsync(id);
            
            if(chapter == null )
            {
                return null;
            }
            
            return chapter;
        }

        public async Task<ChapterModel> createVideoAndRelateItToChapter(int id, CreateVideoDto videoDto)
        {
            
            if (id == 0 || videoDto == null)
            {
                return null;
            }

            ChapterModel chapter = await _chapterRepository.ObtainAsync(c => c.id == id);

            if (chapter == null)
            {
                return null;
            }    

            VideoModel videoModel = _mapper.Map<VideoModel>(videoDto);
            videoModel.ChapterModel = chapter;

            await _videoRepository.CreateAsync(videoModel);

            return chapter;
        } 
    }
}