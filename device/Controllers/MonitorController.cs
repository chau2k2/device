using device.Data;
using device.DTO.Monitor;
using device.IRepository;
using device.Models;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : ControllerBase
    {
        private readonly MonitorService _service;
        public MonitorController(ILogger<MonitorService> logger, LaptopDbContext context, IAllRepository<MonitorM> repos)
        {
            _service = new MonitorService(repos, logger, context);
        }

        [HttpGet("get_all_monitor")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPut("update_monitor")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMonitor UMn)
        {
            return Ok (await _service.Update(id, UMn));
        }

        [HttpGet("get_by_id")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok ( await _service.GetById(id));
        }

        [HttpPost("create_monitor")]
        public async Task<IActionResult> Create([FromBody] CreateMonitor CMn)
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
