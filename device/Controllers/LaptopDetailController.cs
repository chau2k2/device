using device.IServices;
using device.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopDetailController : ControllerBase
    {
        private readonly IAllService<LaptopDetail> _service;
        public LaptopDetailController(IAllService<LaptopDetail> service)
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
                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] LaptopDetail laptopDetail)
        {
            var result = await _service.Add(laptopDetail);
            return Ok(result);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LaptopDetail laptopDetail)
        {
            var result = await _service.Update(id, laptopDetail);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.Delete(id));
        }
    }
}
