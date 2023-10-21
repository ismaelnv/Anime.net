using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{
    [ApiController]
    [Route("/api/animes")]
    public class AnimeController : Controller
    {
        private readonly ILogger<AnimeController> _logger;
        private AnimeService _animeService;
        private ChapterService _chapterService;

        public AnimeController(ILogger<AnimeController> logger, AnimeService animeService,ChapterService chapterService)
        {
            _logger = logger;
            _animeService = animeService;
            _chapterService = chapterService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AnimeDto>>> getAnimes()
        {
            return Ok(await _animeService.getAnimes());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AnimeModel>> createAnime(AnimeModel anime)
        {
            if (anime == null)
            {
                return BadRequest();
            }

            var newAnime = await _animeService.createAnime(anime);

            return Created(string.Empty, newAnime);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AnimeModel>> updateAnime(int id, [FromBody] AnimeModel anime)
        {

            if (id != anime.Id)
            {
                return BadRequest();
            }

            var updatedAnime = await _animeService.updateAnime(id, anime);

            return Ok(updatedAnime);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AnimeModel>> getAnimeId(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var anime = await _animeService.getAnimeId(id);

            if (anime == null)
            {
                return NotFound();
            }
            return Ok(anime);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AnimeModel>> removeAnime(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }    

            var anime = await _animeService.removeAnime(id);
            return Ok(anime) ;
        }

        [HttpGet("{id}/capitulos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AnimeModel>> getAnimeChapters(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            AnimeModel animeCapitulos = await _animeService.getAnimeChapters(id);

            if ( animeCapitulos == null )
            {
                return  NotFound();
            }

            return Ok(animeCapitulos);
        }

        [HttpPost("{id}/capitulos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AnimeModel>> createAChapterAndRelateItToAnime(int id,[FromBody] ChapterDto chapterDto)
        { 
            if (id == 0 || chapterDto == null)
            {
                return BadRequest();
            }

            var anime = await _animeService.getAnimeId(id);

            if (anime == null)
            {
                return NotFound();
            }

            ChapterModel chapter = new (){

                title = chapterDto.title,
                episode = chapterDto.episode,
                description = chapterDto.description,
                AnimeModel = anime 
            };
            
            await _chapterService.createChapter(chapter);

            return Created(string.Empty,anime);
        }

    }
}