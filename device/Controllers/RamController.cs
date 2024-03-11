using device.Data;
using device.DTO.Ram;
using device.IRepository;
using device.Entity;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/ram")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly RamService _service;

        public RamController(ILogger<RamService> logger, LaptopDbContext context, IAllRepository<Ram> repo)
        {
            _service = new RamService(repo, logger, context);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPut("do-update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRam udR)
        {
            return Ok (await _service.Update(id, udR));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok (await _service.GetById(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateRam CrR)
        {
            return Ok (await _service.Create(CrR)); 
        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            return Ok (await _service.delete(id));
        }
    }
}
