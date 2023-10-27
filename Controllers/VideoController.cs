using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : Controller
    {

        private readonly ILogger<VideoController> _logger;
        private IVideoService _videoService;
        private IMapper _mapper;

        public VideoController(ILogger<VideoController> logger, IVideoService videoService, IMapper mapper)
        {
            
            _logger = logger;
            _videoService = videoService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VideoDto>>> getVideos()
        {
            return Ok(await _videoService.getVideos());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VideoDto>> createVideo([FromBody] CreateVideoDto createVideoDto)
        {
            try
            {

                VideoModel? video = await _videoService.CreateVideo(createVideoDto);
                VideoDto videoDto = _mapper.Map<VideoDto>(video);
                return Created(string.Empty, videoDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VideoDto>> updateVideo(int id, [FromBody] UpdateVideoDto updateVideoDto)
        {
            try
            {

                VideoModel? video = await _videoService.updateVideo(id, updateVideoDto);
                VideoDto videoDto = _mapper.Map<VideoDto>(video);

                return Ok(videoDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VideoDto>> getVideoId(int id)
        {
            try
            {

                VideoModel? video = await _videoService.getVideoId(id);

                if (video == null)
                {
                    return NotFound("Video not found");
                }

                return Ok(video);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VideoDto>> removeVideo(int id)
        {
            try
            {

                VideoModel? video = await _videoService.removeVideo(id);

                if (video == null)
                {
                    return NotFound("The video you want to delete was not found");
                }

                VideoDto videoDto = _mapper.Map<VideoDto>(video);
                return Ok(videoDto);
            }
            catch(Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

    }
}