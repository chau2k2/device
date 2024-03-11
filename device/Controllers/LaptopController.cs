using device.Data;
using device.DTO.Laptop;
using device.IRepository;
using device.Entity;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/laptop")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly LaptopService _service;

        public LaptopController(LaptopDbContext context, IAllRepository<Laptop> repos, ILogger<LaptopService> logger)
        {
            _service = new LaptopService(repos, logger, context);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAllLaptop(page, pageSize));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetOne(int id)
        {
            return Ok ( await _service.GetLaptopById(id));
        }

        [HttpPost("create-laptop")]
        public async Task<IActionResult> Create([FromBody] CreateLaptop CrL)
        {
           return Ok(await _service.CreateLaptop(CrL));
        }

        [HttpPost("update-id")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateLaptop upl)
        {
            return Ok(await _service.Updatelaptop(id, upl));
        }
        [HttpDelete("delete-laptop")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok (await _service.DeleteLaptop(id));
        }
    }
}
