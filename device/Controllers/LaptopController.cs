using device.Data;
using device.DTO.Laptop;
using device.IRepository;
using device.Models;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly LaptopService _service;

        public LaptopController(LaptopDbContext context, IAllRepository<Laptop> repos, ILogger<LaptopService> logger)
        {
            _service = new LaptopService(repos, logger, context);
        }

        [HttpGet("get_all_laptop")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAllLaptop(page, pageSize));
        }

        [HttpGet("get_laptop_id")]
        public async Task<IActionResult> GetOne(int id)
        {
            return Ok ( await _service.GetLaptopById(id));
        }

        [HttpPost("create_laptop")]
        public async Task<IActionResult> Create([FromBody] CreateLaptop CrL)
        {
           return Ok(await _service.CreateLaptop(CrL));
        }

        [HttpPost("update_id")]
        public async Task<IActionResult> Update(int id)
        {
            return Ok(await _service.DeleteLaptop(id));
        }
        [HttpDelete("delete_laptop")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok (await _service.DeleteLaptop(id));
        }
    }
}
