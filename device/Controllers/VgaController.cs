using device.Data;
using device.DTO.Vga;
using device.IRepository;
using device.Models;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vga>>> SelectAllVga(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult<Vga>> CreateVga(CreateVga vgs)
        {
            return Ok(await _service.Create(vgs));
        }

        [HttpPut]
        public async Task<ActionResult<Vga>> UpdateVga(int id, UpdateVga Uvga)
        {
            return Ok (await _service .Update(id, Uvga));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVga(int id)
        {
            return Ok (await _service.delete(id));
        }
    }
}
