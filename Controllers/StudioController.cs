using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{

    [ApiController]
    [Route("api/studios")]
    public class StudioController : Controller
    {
        private readonly ILogger<StudioController> _logger;
        private readonly IStudioService _studioService;
        private IMapper _mapper;

        public StudioController(ILogger<StudioController> logger,IStudioService studioService,IMapper mapper)
        {

            _logger = logger;
            _studioService = studioService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StudioDto>>> getGenres()
        {

            return Ok(await _studioService.getStudios());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudioDto>> createStudio(CreateStudioDto createStudioDto)
        {
            try
            {

                StudioModel studioModel = await _studioService.createStudio(createStudioDto);
                StudioDto studio = _mapper.Map<StudioDto>(studioModel);

                return Created(string.Empty, studio);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StudioDto>> updateStudio(int id, [FromBody] UpdateStudioDto updateStudioDto)
        {
            try
            {

                StudioModel? studio = await _studioService.updateStudio(id,updateStudioDto);
                StudioDto studioDto = _mapper.Map<StudioDto>(studio);
                return Ok(studio);
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
        public async Task<ActionResult<StudioDto>> getStudioId(int id)
        {

            try
            {
                StudioModel? studio = await _studioService.getStudioId(id);

                if (studio == null)
                {
                    return NotFound("The studio you want to search for was not found");
                }

                StudioDto studioDto = _mapper.Map<StudioDto>(studio);
                return Ok(studio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudioDto>> removeStudio(int id)
        {
            try
            {

                StudioModel? studio = await _studioService.removeStudio(id);

                if (studio == null)
                {
                    return NotFound("The studio you want to delete was not found");
                }

                StudioDto studioDto = _mapper.Map<StudioDto>(studio);
                return Ok(studio);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/animes")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<StudioModel>> getStudioAndAnimes(int id)
        {
            try
            {

                StudioModel? studio = await _studioService.getStudioAndAnimes(id);

                if (studio == null)
                {
                    return NotFound("Studio not found");
                }

                return Ok(studio);
            }
            catch(Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}