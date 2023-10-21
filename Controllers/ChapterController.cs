using AnimeWeb.Models;
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
        public async Task<ActionResult<IEnumerable<ChapterModel>>> getCApitulos()
        {
            return Ok(await _chapterService.getChapters());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChapterModel>> createCapitulo([FromBody] ChapterModel chapter)
        {
            if (chapter == null)
            {
                return BadRequest();
            }

            var newCapitulo = await _chapterService.createChapter(chapter);

            return Created(string.Empty, newCapitulo);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChapterModel>> updateCapitulo(int id, [FromBody] ChapterModel chapter)
        {

            if (id != chapter.id)
            {
                return BadRequest();
            }

            var updateCapitulo = await _chapterService.updateChapter(id, chapter);

            return Ok(updateCapitulo);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChapterModel>> getCapituloId(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var capitulo = await _chapterService.getChapterId(id);

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
        public async Task<ActionResult<ChapterModel>> removeCapitulo(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }    

            var capitulo = await _chapterService.removeChapter(id);
            return Ok(capitulo);
        }
    }
}