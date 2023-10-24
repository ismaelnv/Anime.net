using AnimeWeb.Models.Dto;
using AnimeWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : Controller
    {
        private readonly ILogger<VideoController> _logger;
        private readonly VideoService _videoService;

        public VideoController(ILogger<VideoController> logger, VideoService videoService)
        {
            _logger = logger;
            _videoService = videoService;
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
            if (createVideoDto== null)
            {
                return BadRequest();
            }

            VideoDto video = await _videoService.CreateVideo(createVideoDto);
            return Created(string.Empty, video);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VideoDto>> updateVideo(int id, [FromBody] UpdateVideoDto updateVideoDto)
        {

            if (id != updateVideoDto.id)
            {
                return BadRequest();
            }

            VideoDto video = await _videoService.updateVideo(id,updateVideoDto);

            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VideoDto>> getVideoId(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            VideoDto video = await _videoService.getVideoId(id);

            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VideoDto>> removeVideo(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }    

            VideoDto video = await _videoService.removeVideo(id);
            return Ok(video);
        }

    }
}