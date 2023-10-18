using AnimeWeb.Models;
using AnimeWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Controllers
{   
    [ApiController]
    [Route("/api/capitulos")]
    public class CapituloController : Controller
    {
        private readonly ILogger<CapituloController> _logger;
        private CapituloService _capituloService;

        public CapituloController(ILogger<CapituloController> logger, CapituloService capituloService)
        {
            _logger = logger;
            _capituloService = capituloService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CapituloModel>>> getCApitulos()
        {
            return Ok(await _capituloService.getCapitulos());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CapituloModel>> createCapitulo([FromBody] CapituloModel capitulo)
        {
            if (capitulo == null)
            {
                return BadRequest();
            }

            var newCapitulo = await _capituloService.createCapitulo(capitulo);

            return Ok(newCapitulo);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CapituloModel>> updateCapitulo(int id, [FromBody] CapituloModel capitulo)
        {

            if (id != capitulo.id)
            {
                return BadRequest();
            }

            var updateCapitulo = await _capituloService.updateCapitulo(id, capitulo);

            return Ok(updateCapitulo);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CapituloModel>> getCapituloId(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var capitulo = await _capituloService.getCapituloId(id);

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
        public async Task<ActionResult<CapituloModel>> removeCapitulo(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }    

            var capitulo = await _capituloService.removeCapitulo(id);
            return Ok(capitulo);
        }
    }
}