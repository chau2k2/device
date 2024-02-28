using device.Data;
using device.DTO.Ram;
using device.IRepository;
using device.Models;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly RamService _service;

        public RamController(ILogger<RamService> logger, LaptopDbContext context, IAllRepository<Ram> repo)
        {
            _service = new RamService(repo, logger, context);
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPut("do_update_ram")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRam udR)
        {
            return Ok (await _service.Update(id, udR));
        }

        [HttpGet("get_by_id")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok (await _service.GetById(id));
        }

        [HttpPost("create_ram")]
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
