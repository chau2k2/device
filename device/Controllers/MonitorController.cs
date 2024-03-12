using Microsoft.AspNetCore.Mvc;
using device.ModelResponse;
using device.IServices;

namespace device.Controllers
{
    [Route("api/monitor")]
    [ApiController]
    public class MonitorController : ControllerBase
    {
        private readonly IMonitorService _service;
        public MonitorController(IMonitorService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromBody] MonitorResponse UMn)
        {
            return Ok (await _service.Update(id, UMn));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok ( await _service.GetById(id));
        }

        [HttpPost("create-monitor")]
        public async Task<IActionResult> Create([FromBody] MonitorResponse CMn)
        {
            return Ok (await _service.Create(CMn));
        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            return Ok ( await _service.Delete(id));
        }
    }
}
