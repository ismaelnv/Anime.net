using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using AnimeWeb.Service.Interface;
using AutoMapper;

namespace AnimeWeb.Service
{
    public class VideoService : IVideoService
    {
        
        private readonly IVideoRepository _videoRepository;
        private readonly IChapterService _chapterService;
        private readonly IMapper _mapper;

        public VideoService(IVideoRepository videoRepository, IChapterService chapterService,IMapper mapper)
        {

            _videoRepository = videoRepository;
            _chapterService = chapterService;
            _mapper = mapper;
        }   

        public async Task<IEnumerable<VideoDto>> getVideos()
        {
            IEnumerable<VideoModel> videoModels = await _videoRepository.GetAllAsync();
            IEnumerable<VideoDto> videos = _mapper.Map<IEnumerable<VideoDto>>(videoModels);
            return videos;
        }

        public async Task<VideoModel?> getVideoId(int id)
        {

            if(id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            VideoModel video = await _videoRepository.ObtainAsync(v => v.id == id);

            if (video == null)
            {
                return null;
            }

            return video;
        }

        public async Task<VideoModel?> CreateVideo(CreateVideoDto createVideoDto)
        {

            if(createVideoDto == null)
            {
                throw new BadHttpRequestException("Invalid Video");
            }

            ChapterModel? chapterModel = await _chapterService.getChapterId(createVideoDto.idChapter);

            if (chapterModel == null)
            {
                return null;
            }

            VideoModel video = _mapper.Map<VideoModel>(createVideoDto);
            video.ChapterModel = chapterModel;
            video.uploadDate = DateTime.Now;
            video.updateDate = DateTime.Now;

            await _videoRepository.CreateAsync(video);

            return video;
        } 

        public async Task<VideoModel?> removeVideo(int id)
        {

            if (id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            VideoModel? video = await this.getVideoId(id);

            if (video == null)
            {
                return null;
            }

            await _videoRepository.RemoveAsync(video);
            
            return video;
        }

        public async Task<VideoModel?> updateVideo(int id, UpdateVideoDto updateVideoDto)
        {

            if (updateVideoDto.id != id)
            {
                throw new BadHttpRequestException("Id does not match the video id");
            }

            if (updateVideoDto == null)
            {
                throw new BadHttpRequestException("Invalid video"); 
            }

            VideoModel videoModel = _mapper.Map<VideoModel>(updateVideoDto);
            VideoModel video = await _videoRepository.UpdateAsync(videoModel);
            
            return video;
        }
    }
}