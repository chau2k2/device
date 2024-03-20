using device.Data;
using device.IRepository;
using device.Entity;
using device.Services;
using Microsoft.AspNetCore.Mvc;
using device.Models;

namespace device.Controllers
{
    [Route("api/laptop")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly LaptopService _service;
        private readonly ILogger<LaptopController> _logger;

        public LaptopController(LaptopDbContext context, IAllRepository<Laptop> repos, ILogger<LaptopController> logger)
        {
            _service = new LaptopService(repos, context);
            _logger = logger;
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
        public async Task<IActionResult> Create([FromBody] LaptopModel CrL)
        {
           return Ok(await _service.CreateLaptop(CrL));
        }

        [HttpPost("update-id")]
        public async Task<IActionResult> Update(int id,[FromBody] LaptopModel upl)
        {
            return Ok(await _service.Updatelaptop(id, upl));
        }

        [HttpDelete("delete-laptop")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok (await _service.DeleteLaptop(id));
        }
        [HttpGet("get-laptop-by-name")]
        public async Task<IActionResult> FindLaptopByName (string name)
        {
            return Ok (await _service.FindLaptopByName(name));
        }
    }
}
