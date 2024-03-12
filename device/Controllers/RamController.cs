using Microsoft.AspNetCore.Mvc;
using device.ModelResponse;
using device.IServices;

namespace device.Controllers
{
    [Route("api/ram")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly IRamService _service;

        public RamController(IRamService service)
        {
            _service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPut("do-update")]
        public async Task<IActionResult> Update(int id, [FromBody] RamResponse udR)
        {
            return Ok (await _service.Update(id, udR));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok (await _service.GetById(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RamResponse CrR)
        {
            return Ok (await _service.Create(CrR)); 
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> delete(int id)
        {
            return Ok (await _service.delete(id));
        }
    }
}
