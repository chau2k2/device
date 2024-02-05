using device.IServices;
using device.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoHangController : ControllerBase
    {
        private readonly ILogger<KhoHangController> logger;
        private readonly IAllService<KhoHang> _service;
        public KhoHangController(ILogger<KhoHangController> logger, IAllService<KhoHang> service)
        {
            this.logger = logger;
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _service.GetAll(page, pageSize);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] KhoHang khohang)
        {
            var result = await _service.Update(id, khohang);
            return Ok(result);
        }
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] KhoHang khohang)
        {
            var result = await _service.Add(khohang);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var del = await _service.Delete(id);
            if (del == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
