using device.Entity;
using Microsoft.AspNetCore.Mvc;
using device.ModelResponse;
using device.IServices;

namespace device.Controllers
{
    [Route("api/vga")]
    [ApiController]
    public class VgaController : ControllerBase
    {
        private readonly IVgaService _service;

        public VgaController(IVgaService vgaService) 
        {
            _service = vgaService;
        }

        [HttpGet("get_all")]
        public async Task<ActionResult<IEnumerable<Vga>>> SelectAllVga(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPost("do_create")]
        public async Task<ActionResult<Vga>> CreateVga(VgaResponse vgs)
        {
            return Ok(await _service.Create(vgs));
        }

        [HttpPut("do_update")]
        public async Task<ActionResult<Vga>> UpdateVga(int id, VgaResponse Uvga)
        {
            return Ok (await _service .Update(id, Uvga));
        }

        [HttpDelete("do_delete")]
        public async Task<IActionResult> DeleteVga(int id)
        {
            return Ok (await _service.delete(id));
        }
        [HttpGet("get-by-id")]
        public async Task<ActionResult<Vga>> GetById (int id)
        {
            return 
        }
    }
}
