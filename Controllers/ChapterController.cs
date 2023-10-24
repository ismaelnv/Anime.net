using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{   
    [ApiController]
    [Route("/api/chapters")]
    public class ChapterController : Controller
    {
        private readonly ILogger<ChapterController> _logger;
        private ChapterService _chapterService;

        public ChapterController(ILogger<ChapterController> logger, ChapterService chapterService)
        {
            _logger = logger;
            _chapterService = chapterService;
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
        public async Task<ActionResult<ChapterDto>> createChapter([FromBody] CreateChapterDto chapterDto)
        {
            if (chapterDto== null)
            {
                return BadRequest();
            }

            ChapterDto newChapter = await _chapterService.createChapter(chapterDto);

            return Created(string.Empty, newChapter);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterDto>> updateChapter(int id, [FromBody] UpdateChapterDto updateChapterDto)
        {

            if (id != updateChapterDto.id)
            {
                return BadRequest();
            }

            ChapterDto chapter = await _chapterService.updateChapter(id, updateChapterDto);

            if ( chapter == null)
            {
                return NotFound();
            }

            return Ok(chapter);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterDto>> getChapterId(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            ChapterDto capitulo = await _chapterService.getChapterId(id);

            if (capitulo == null)
            {
                return NotFound();
            }

            return Ok(capitulo);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChapterDto>> removeChapter(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }    

            ChapterDto chapter = await _chapterService.removeChapter(id);
            return Ok(chapter);
        }

        [HttpGet("{id}/videos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterModel>> getChapterVideos(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            ChapterModel chapter = await _chapterService.getChapterCapitulos(id);

            if ( chapter == null )
            {
                return  NotFound();
            }

            return Ok(chapter);
        }

        [HttpPost("{id}/videos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterModel>> createAVideoAndRelateItToChapter(int id, [FromBody] CreateVideoDto videoDto)
        { 
            if (id == 0 || videoDto == null)
            {
                return BadRequest();
            }

            ChapterModel chapter = await _chapterService.createVideoAndRelateItToChapter(id,videoDto);

            return Created(string.Empty,chapter);
        }

    }
}