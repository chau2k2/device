using device.Data;
using device.DTO.Vga;
using device.IRepository;
using device.Entity;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VgaController : ControllerBase
    {
        private readonly VgaService _service;

        public VgaController(ILogger<VgaService> logger, IAllRepository<Vga> repo, LaptopDbContext context) 
        {
            _service = new VgaService(repo,logger, context);
        }

        [HttpGet("get_all")]
        public async Task<ActionResult<IEnumerable<Vga>>> SelectAllVga(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPost("do_create")]
        public async Task<ActionResult<Vga>> CreateVga(CreateVga vgs)
        {
            return Ok(await _service.Create(vgs));
        }

        [HttpPut("do_update")]
        public async Task<ActionResult<Vga>> UpdateVga(int id, UpdateVga Uvga)
        {
            return Ok (await _service .Update(id, Uvga));
        }

        [HttpDelete("do_delete")]
        public async Task<IActionResult> DeleteVga(int id)
        {
            return Ok (await _service.delete(id));
        }
    }
}
