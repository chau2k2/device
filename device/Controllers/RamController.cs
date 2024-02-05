using device.IServices;
using device.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly ILogger<RamController> logger;
        private readonly IAllService<Ram> _service;
        public RamController(ILogger<RamController> logger, IAllService<Ram> service)
        {
            this.logger = logger;
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _service.GetAll(page, pageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] Ram ram)
        {
            var result = await _service.Update(id, ram);
            return Ok(result);
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Ram ram)
        {
            var result = await _service.Add(ram);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var del = await _service.Delete(id);
            if (del == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
