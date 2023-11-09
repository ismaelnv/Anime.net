using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Service;
using AnimeWeb.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{
    [ApiController]
    [Route("/api/genres")]
    public class GenreController : Controller
    {
        
        private readonly ILogger<GenreController> _logger;
        private IGenreService _genreService;
        private IAnimeService _animeService;
        private IMapper _mapper;

        public GenreController(ILogger<GenreController> logger, IMapper mapper, IGenreService genreService,IAnimeService animeService)
        {

            _logger = logger;
            _mapper = mapper;
            _genreService = genreService;
            _animeService = animeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GenreDto>>> getGenres()
        {
            return Ok(await _genreService.getGenres());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenreDto>> createGenre(CreateGenreDto createGenreDto)
        {
            try
            {

                GenreModel genreModel = await _genreService.createGenre(createGenreDto);
                GenreDto genre = _mapper.Map<GenreDto>(genreModel);

                return Created(string.Empty, genre);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GenreDto>> updateGenre(int id, [FromBody] updateGenreDto updateGenreDto)
        {
            try
            {

                GenreModel? genre = await _genreService.updateGenre(id,updateGenreDto);
                GenreDto genreDto = _mapper.Map<GenreDto>(genre);
                return Ok(genre);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreDto>> getGenreId(int id)
        {

            try
            {
                GenreModel? genre = await _genreService.getGenreId(id);

                if (genre == null)
                {
                    return NotFound("The genre you want to search for was not found");
                }

                GenreDto genreDto = _mapper.Map<GenreDto>(genre);
                return Ok(genreDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenreDto>> removeGenre(int id)
        {
            try
            {

                GenreModel? genre = await _genreService.removeGenre(id);

                if (genre == null)
                {
                    return NotFound("The genre you want to delete was not found");
                }

                GenreDto genreDto = _mapper.Map<GenreDto>(genre);
                return Ok(genreDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/animes")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreModel>> getGenreAnimes(int id)
        {
            try
            {
                GenreModel? genreModelAnimes = await _genreService.getGenreAnimes(id);

                if (genreModelAnimes == null)
                {
                    return NotFound("Genre not found");
                }

                return Ok(genreModelAnimes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/animes") ]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GenreModel?>> createAnimeAndRelateItToGenre(int id,[FromBody] CreateAnimeDto createAnimeDto)
        {

            try
            {
                if ( id == 0)
                {
                    throw new BadHttpRequestException("Id invalid");
                }

                GenreModel? genre = await _genreService.getGenreId(id);

                if (genre == null)
                {
                    return NotFound("The genre was not found searching");
                }

                AnimeModel anime = _mapper.Map<AnimeModel>(createAnimeDto);
                anime.Genres.Add(genre);

                CreateAnimeDto animeDto = _mapper.Map<CreateAnimeDto>(anime);

                await _animeService.createAnime(animeDto);
                return Ok(genre);
            }
            catch(Exception e)
            {

                return BadRequest(e.Message); 
            }
        }  

        [HttpGet("animes/{nameGenre}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AnimeDto?>>> getAnimesByGenre(string nameGenre)
        {
            try
            {

                IEnumerable<AnimeModel?> animes = await _genreService.getAnimesByGenre(nameGenre);
                IEnumerable<AnimeDto>animeDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);
                return Ok(animeDto);
            }
            catch(Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
    }
}