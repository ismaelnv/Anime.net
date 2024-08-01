using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{
    [ApiController]
    [Route("/api/chapters")]
    public class ChapterController : Controller
    {

        private readonly ILogger<ChapterController> _logger;
        private IChapterService _chapterService;
        private IVideoService _videoService;
        private IMapper _mapper;

        public ChapterController(ILogger<ChapterController> logger, IChapterService chapterService, IMapper mapper,IVideoService videoService)
        {
            
            _logger = logger;
            _chapterService = chapterService;
            _mapper = mapper;
            _videoService = videoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> getChapters()
        {

            return Ok(await _chapterService.getChapters());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChapterDto>> createChapter([FromBody] CreateChapterDto createChapterDto)
        {
            try
            {

                ChapterModel? Chapter = await _chapterService.createChapter(createChapterDto);
                ChapterDto chapterDto = _mapper.Map<ChapterDto>(Chapter);

                return Created(string.Empty, chapterDto);
            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterDto>> updateChapter(int id, [FromBody] UpdateChapterDto updateChapterDto)
        {
            try
            {

                ChapterModel? chapter = await _chapterService.updateChapter(id, updateChapterDto);

                if (chapter == null)
                {
                    return NotFound("The chapter you want to update could not be found");
                }

                ChapterDto chapterDto = _mapper.Map<ChapterDto>(chapter);
                return Ok(chapterDto);
            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }

      
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterDto>> getChapterId(int id)
        {
            try
            {

                ChapterModel? chapter = await _chapterService.getChapterId(id);

                if (chapter == null)
                {
                    return NotFound("Chapter not found");
                }

                ChapterDto chapterDto = _mapper.Map<ChapterDto>(chapter);

                return Ok(chapterDto);
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
        public async Task<ActionResult<ChapterDto>> removeChapter(int id)
        {
            try
            {

                ChapterModel? chapter = await _chapterService.removeChapter(id);

                if (chapter == null)
                {
                    return NotFound("Could not find the chapter you want to delete");
                }
                
                ChapterDto chapterDto = _mapper.Map<ChapterDto>(chapter);
                return Ok(chapterDto);
            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/videos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterModel>> getChapterVideos(int id)
        {
            try
            {
                
                ChapterModel? chapter = await _chapterService.getChapterVideos(id);

                if (chapter == null)
                {
                    return NotFound("The chapter you want to update was not found");
                }

                return Ok(chapter);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/videos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterModel>> createAVideoAndRelateItToChapter(int id, [FromBody] CreateVideoDto createVideoDto)
        {
            try
            {

                if (id == 0)
                {
                    throw new BadHttpRequestException("Invalid Id");
                }

                if (createVideoDto == null)
                {
                    throw new BadHttpRequestException("Invalid video");
                }

                ChapterModel? chapter = await _chapterService.getChapterId(id);

                if (chapter == null)
                {
                    return NotFound("The chapter was not found");
                }  

                VideoModel videoModel = _mapper.Map<VideoModel>(createVideoDto);
                videoModel.ChapterModel = chapter;

                CreateVideoDto createVideo = _mapper.Map<CreateVideoDto>(videoModel);

                VideoModel? video  = await _videoService.CreateVideo(createVideo);

                if (video == null)
                {
                    return NotFound("Error creating video");
                }
                
                return chapter;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("fromLowestToHighest")]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> orderTheChaptersFromSmallestToLargest()
        {

            IEnumerable<ChapterDto> chapters = await _chapterService.orderTheChaptersFromSmallestToLargest();
            return Ok(chapters);
        }
    }
}