using device.Data;
using device.DTO.Producer;
using device.IRepository;
using device.Entity;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/producer")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly ProducerService _service;

        public ProducerController( LaptopDbContext context, IAllRepository<Producer> repos, ILogger<ProducerService> logger)
        {
            _service = new ProducerService(repos,logger, context);
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
        public async Task< ActionResult> Update(int id, [FromBody] UpdateProducer Upd)
        {
            if (!ModelState.IsValid)
            {
                StatusCode(StatusCodes.Status400BadRequest, "Error Request");
            }
            return Ok(await _service.UpdateProducer(id, Upd));
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateProducer([FromBody]CreateProducer cpr)
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
