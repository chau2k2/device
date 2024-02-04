using device.IServices;
using device.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly IAllService<Laptop> _service;

        public LaptopController(IAllService<Laptop> service)
        {
            _service = service;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            try
            {
                var result = await _service.GetAll(page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                var result = await _service.GetById<Producer>($"http://localhost:5272/api/Laptop/GetById/{id}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Laptop laptop)
        {
            try
            {
                var result = await _service.Add(laptop);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Laptop laptop)
        {
            var result = await _service.Update(id, laptop);
            return Ok(result);
        }
        //public async Task<IActionResult> Delete (int id)
        //{
        //    var result = await _service.Delete("https://localhost:7121/device/", "https://localhost:7121", id);
        //}
    }
}
