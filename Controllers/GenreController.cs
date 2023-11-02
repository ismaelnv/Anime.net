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
        private IGenreService _categorieService;
        private IAnimeService _animeService;
        private IMapper _mapper;

        public GenreController(ILogger<GenreController> logger, IMapper mapper, IGenreService categorieService,IAnimeService animeService)
        {

            _logger = logger;
            _mapper = mapper;
            _categorieService = categorieService;
            _animeService = animeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GenreDto>>> getCategorie()
        {
            return Ok(await _categorieService.getCategories());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenreDto>> createCategorie(CreateGenreDto createCategorieDto)
        {
            try
            {

                GenreModel categorieModel = await _categorieService.createCategorie(createCategorieDto);
                GenreDto categorie = _mapper.Map<GenreDto>(categorieModel);

                return Created(string.Empty, categorie);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GenreDto>> updateCategorie(int id, [FromBody] updateGenreDto updateCategorieDto)
        {
            try
            {

                GenreModel? categorie = await _categorieService.updateCategorie(id,updateCategorieDto);
                GenreDto categorieDto = _mapper.Map<GenreDto>(categorie);
                return Ok(categorie);
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
        public async Task<ActionResult<GenreDto>> getCAtegorieId(int id)
        {

            try
            {
                GenreModel? categorie = await _categorieService.getCategorieId(id);

                if (categorie == null)
                {
                    return NotFound("The categorie you want to search for was not found");
                }

                GenreDto categorieDto = _mapper.Map<GenreDto>(categorie);
                return Ok(categorieDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenreDto>> removeCategorie(int id)
        {
            try
            {

                GenreModel? categorie = await _categorieService.removeCategorie(id);

                if (categorie == null)
                {
                    return NotFound("The categorie you want to delete was not found");
                }

                GenreDto categorieDto = _mapper.Map<GenreDto>(categorie);
                return Ok(categorieDto);
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
                GenreModel? genreModelAnimes = await _categorieService.getGenreAnimes(id);

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
        public async Task<ActionResult<GenreModel?>> createAnimeAndRelateItToGenre(int id,[FromBody] CreateAnimeDto createAnimeDto)
        {

            if ( id == 0)
            {
                return null;
            }

            GenreModel? genre = await _categorieService.getCategorieId(id);

            if (genre == null)
            {
                return null;
            }

            AnimeModel anime = _mapper.Map<AnimeModel>(createAnimeDto);
            anime.Genres.Add(genre);

            CreateAnimeDto animeDto = _mapper.Map<CreateAnimeDto>(anime);

            await _animeService.createAnime(animeDto);
            return genre;
        } 


    }
}