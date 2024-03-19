using device.Entity;
using Microsoft.AspNetCore.Mvc;
using device.ModelResponse;
using device.IServices;
using device.Models;

namespace device.Controllers
{
    [Route("api/vga")]
    [ApiController]
    public class VgaController : ControllerBase
    {
        private readonly IVgaService _service;
        private readonly ILogger<VgaController> _logger;
        public VgaController(IVgaService vgaService, ILogger<VgaController> logger) 
        {
            _service = vgaService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<Vga>>> SelectAllVga(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPost("do-create")]
        public async Task<ActionResult<Vga>> CreateVga(VgaModel vgs)
        {
            return Ok(await _service.Create(vgs));
        }

        [HttpPut("do-update")]
        public async Task<ActionResult<Vga>> UpdateVga(int id, VgaModel Uvga)
        {
            return Ok (await _service .Update(id, Uvga));
        }

        [HttpDelete("do-delete")]
        public async Task<IActionResult> DeleteVga(int id)
        {
            return Ok (await _service.Delete(id));
        }
        [HttpGet("get-by-id")]
        public async Task<ActionResult<Vga>> GetById (int id)
        {
            return Ok ( await _service.GetById(id));
        }
    }
}
