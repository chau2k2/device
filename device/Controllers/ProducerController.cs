using Microsoft.AspNetCore.Mvc;
using device.ModelResponse;
using device.IServices;

namespace device.Controllers
{
    [Route("api/producer")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _service;
        private readonly ILogger<ProducerController> _logger;

        public ProducerController( IProducerService service, ILogger<ProducerController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult> FindById(int id)
        {
            return Ok(await _service.GetProducerById(id));
        }

        [HttpPut("do-update")]
        public async Task< ActionResult> Update(int id, [FromBody] ProducerResponse Upd)
        {
            if (!ModelState.IsValid)
            {
                StatusCode(StatusCodes.Status400BadRequest, "Error Request");
            }
            return Ok(await _service.UpdateProducer(id, Upd));
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateProducer([FromBody] ProducerResponse cpr)
        {
            if (!ModelState.IsValid)
            {
                StatusCode(StatusCodes.Status400BadRequest, "Error Request");
            }
            return Ok(await _service.CreateProducer(cpr));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> delete(int id)
        {
            return Ok (await _service.DeleteProducer(id));
        }
    }
}
