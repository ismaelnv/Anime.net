using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Service;
using AnimeWeb.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{
    [ApiController]
    [Route("/api/animes")]
    public class AnimeController : Controller
    {
        
        private readonly ILogger<AnimeController> _logger;
        private IAnimeService _animeService;
        private IChapterService _chapterService;
        private IMapper _mapper;

        public AnimeController(ILogger<AnimeController> logger, IAnimeService animeService, IMapper mapper, IChapterService chapterService)
        {

            _logger = logger;
            _animeService = animeService;
            _mapper = mapper;
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
        public async Task<ActionResult<AnimeDto>> createAnime(CreateAnimeDto createAnimeDto)
        {
            try
            {

                AnimeModel? animeModel = await _animeService.createAnime(createAnimeDto);
                AnimeDto anime = _mapper.Map<AnimeDto>(animeModel);

                return Created(string.Empty, anime);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AnimeDto>> updateAnime(int id, [FromBody] UpdateAnimeDto updateAnimeDto)
        {
            try
            {

                AnimeModel? anime = await _animeService.updateAnime(id, updateAnimeDto);
                AnimeDto animeDto = _mapper.Map<AnimeDto>(anime);
                return Ok(animeDto);
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
        public async Task<ActionResult<AnimeDto>> getAnimeId(int id)
        {

            try
            {
                var anime = await _animeService.getAnimeId(id);

                if (anime == null)
                {
                    return NotFound("The anime you want to search for was not found");
                }

                AnimeDto animeDto = _mapper.Map<AnimeDto>(anime);
                return Ok(animeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AnimeDto>> removeAnime(int id)
        {
            try
            {

                AnimeModel? anime = await _animeService.removeAnime(id);

                if (anime == null)
                {
                    return NotFound("The anime you want to delete was not found");
                }

                AnimeDto animeDto = _mapper.Map<AnimeDto>(anime);
                return Ok(animeDto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/capitulos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AnimeModel>> getAnimeChapters(int id)
        {
            try
            {
                AnimeModel? animeChapters = await _animeService.getAnimeChapters(id);

                if (animeChapters == null)
                {
                    return NotFound("Anime not found");
                }

                return Ok(animeChapters);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/capitulos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AnimeModel>> createAChapterAndRelateItToAnime(int id, [FromBody] CreateChapterDto createChapterDto)
        {
            try
            {

                if (id == 0 || createChapterDto == null)
                {
                    throw new BadHttpRequestException("Id invalid o Invalid chapter");
                }

                AnimeModel? anime = await _animeService.getAnimeId(id);

                if (anime == null)
                {
                    return NotFound("Anime not found");
                }

                ChapterModel chapterModel = _mapper.Map<ChapterModel>(createChapterDto);
                chapterModel.AnimeModel = anime;

                CreateChapterDto chapterDto = _mapper.Map<CreateChapterDto>(chapterModel);

                await _chapterService.createChapter(chapterDto);

                return anime;
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{animeId}/genre/{genreId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AnimeModel>> relateAnimeAndGenre(int animeId, int genreId)
        {
            try
            {

                AnimeModel animeModel = await _animeService.relateAnimesAndGenres(animeId,genreId);

                if (animeModel == null)
                {
                    return NotFound("The genre or anime you want to search for was not found");
                }

                return animeModel;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}