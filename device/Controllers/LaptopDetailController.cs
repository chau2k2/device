using device.Data;
using device.DTO.LaptopDetail;
using device.IRepository;
using device.Models;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopDetailController : ControllerBase
    {
        private readonly LaptopDetailService _service;

        public LaptopDetailController( LaptopDbContext dbContext, ILogger<LaptopDetailService> logger, IAllRepository<LaptopDetail> repos)
        {
            _service = new LaptopDetailService(repos, logger, dbContext);
        }

        [HttpGet("get_all_lap_detail")]
        public async Task<IActionResult> GetAllLapDetail(int page = 1, int pageSize = 5)
        {
            return Ok ( await _service.GetAll(page,pageSize) );
        }

        [HttpGet("get_by_id")]
        public async Task<IActionResult> GetOne(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost("create_laptop_detail")]
        public async Task<IActionResult> Create([FromBody] CreateLaptopDetail CLD)
        {
            return Ok (await _service.Create(CLD));
        }

        [HttpPost("update_lapdetail")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLaptopDetail UDL)
        {
            return Ok (await _service.Update(id, UDL));
        }

        [HttpDelete("do_delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok ( await _service.Delete(id));
        }
    }
}
