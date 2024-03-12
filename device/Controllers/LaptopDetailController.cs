using device.Data;
using device.IRepository;
using device.Entity;
using device.Services;
using Microsoft.AspNetCore.Mvc;
using device.Response;

namespace device.Controllers
{
    [Route("api/laptop-detail")]
    [ApiController]
    public class LaptopDetailController : ControllerBase
    {
        private readonly LaptopDetailService _service;

        public LaptopDetailController( LaptopDbContext dbContext, ILogger<LaptopDetailService> logger, IAllRepository<LaptopDetail> repos)
        {
            _service = new LaptopDetailService(repos, logger, dbContext);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllLapDetail(int page = 1, int pageSize = 5)
        {
            return Ok ( await _service.GetAll(page,pageSize) );
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetOne(int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] LaptopDetailResponse CLD)
        {
            return Ok (await _service.Create(CLD));
        }

        [HttpPost("update-lap-detail")]
        public async Task<IActionResult> Update(int id, [FromBody] LaptopDetailResponse UDL)
        {
            return Ok (await _service.Update(id, UDL));
        }

        [HttpDelete("do-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok ( await _service.Delete(id));
        }
    }
}
