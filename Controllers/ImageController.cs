using AnimeWeb.Service.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{
    [ApiController]
    [Route("/api/images")]
    public class ImageController : Controller
    {

        private readonly ILogger<ImageController> _logger;
        private readonly IImageService _imageService;
        private IMapper _mapper;

        public ImageController(ILogger<ImageController> logger, IMapper mapper,IImageService imageService)
        {
            
            _logger = logger;
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ImageModel>> createImage([FromBody] ImageModel imageModel)
        {
           try
           {

                ImageModel? image = await _imageService.createImage(imageModel);

                return Created(string.Empty, image);
            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ImageModel>>> getAnimes()
        {
            return Ok(await _imageService.getImages());
        }

        [HttpPost("{id}/image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateImage(int id, IFormFile image)
        {
            try
            {

                await _imageService.uploadImage(id,image);
                return Ok();
            }
            catch(Exception e)
            {

                return BadRequest(e.Message);
            }  
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        public async Task<ActionResult<ImageModel>> removeImage(int id)
        {

            try
            {

                ImageModel? image = await _imageService.removeImage(id);

                if (image == null)
                {
                    return NotFound("The image you want to delete was not found");
                }

                return Ok(image);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}        