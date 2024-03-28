using device.Data;
using device.IRepository;
using device.Entity;
using device.Services;
using Microsoft.AspNetCore.Mvc;
using device.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using device.Cons;

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
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            return Ok(await _service.GetAllLaptop(page, pageSize));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetOne(int id)
        {
            return Ok ( await _service.GetLaptopById(id));
        }

        [HttpPost("create-laptop")]
        public async Task<IActionResult> Create([FromBody] LaptopModel model)
        {
           return Ok(await _service.CreateLaptop(model));
        }

        [HttpPost("update-id")]
        public async Task<IActionResult> Update(int id,[FromBody] LaptopModel model)
        {
            return Ok(await _service.Updatelaptop(id, model));
        }

        [HttpDelete("delete-laptop")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok (await _service.DeleteLaptop(id));
        }

        [HttpGet("find-laptop")]
        public async Task<IActionResult> FindLaptop (string name, string producer, decimal firstPrice = 0, decimal endPrice = Constants.MAX_PRICE)
        {
            return Ok (await _service.SearchLaptop(name, producer, firstPrice, endPrice));
        }
    }
}
