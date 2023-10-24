using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using AutoMapper;

namespace AnimeWeb.Service
{
    public class VideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IChapterRepository _chapterRepository;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, IChapterRepository chapterRepository,IMapper mapper)
        {
            _videoRepository = videoRepository;
            _chapterRepository = chapterRepository;
            _mapper = mapper;
        }   

        public async Task<IEnumerable<VideoDto>> getVideos()
        {
            IEnumerable<VideoModel> videoModels = await _videoRepository.GetAllAsync();
            IEnumerable<VideoDto> videos = _mapper.Map<IEnumerable<VideoDto>>(videoModels);
            return videos;
        }

        public async Task<VideoDto> getVideoId(int id)
        {
            if(id == 0){
                return null;
            }

            var video = await _videoRepository.ObtainAsync(v => v.id == id);

            if (video == null)
            {
                return null;
            }

            VideoDto videoDto = _mapper.Map<VideoDto>(video);
            return videoDto;
        }

        public async Task<VideoDto> CreateVideo(CreateVideoDto createVideoDto)
        {
            if(createVideoDto == null)
            {
                return null;
            }

            VideoModel video = _mapper.Map<VideoModel>(createVideoDto);
            video.ChapterModel = await _chapterRepository.ObtainAsync(C => C.id == video.idChapter); 
            
            if(video.ChapterModel == null)
            {
                return null;
            }

            await _videoRepository.CreateAsync(video);
            VideoDto videoDto = _mapper.Map<VideoDto>(video);

            return videoDto;
        } 

        public async Task<VideoDto> removeVideo(int id)
        {
            if (id == 0)
            {
                return null;
            }

            VideoModel video = await _videoRepository.ObtainAsync(v => v.id == id);            
            await _videoRepository.RemoveAsync(video);
            VideoDto videoDto = _mapper.Map<VideoDto>(video);

            return videoDto;
        }

        public async Task<VideoDto> updateVideo(int id, UpdateVideoDto updateVideoDto)
        {
            if (updateVideoDto.id != id || updateVideoDto == null)
            {
                return null;
            }

            VideoModel videoModel = _mapper.Map<VideoModel>(updateVideoDto);
            VideoModel video = await _videoRepository.UpdateAsync(videoModel);

            if (video == null)
            {
                return null;
            }
            
            VideoDto videoDto = _mapper.Map<VideoDto>(video);
            return videoDto;
        }
     
    }
}