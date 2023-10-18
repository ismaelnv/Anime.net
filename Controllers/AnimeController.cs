using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;
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

        public AnimeController(ILogger<AnimeController> logger, IAnimeRepository animeRepository, AnimeService animeService)
        {
            _logger = logger;
            _animeService = animeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AnimeModel>>> getAnimes()
        {
            return Ok(await _animeService.getAnimes());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AnimeModel>> createAnime(AnimeModel anime)
        {
            if (anime == null)
            {
                return BadRequest();
            }

            var newAnime = await _animeService.createAnime(anime);

            return Ok(newAnime);
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

        //metodo esta en prueba a un 
        [HttpGet("{id}/capitulos")]
        public async Task<ActionResult<AnimeModel>> getAnimeCapitulos(int id)
        {
            AnimeModel animeCapitulos = await _animeService.getAnimeCapitulos(id);

            return animeCapitulos;
        }

    }
}