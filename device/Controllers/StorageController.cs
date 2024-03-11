using device.Data;
using device.DTO.Storage;
using device.IRepository;
using device.Entity;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly StorageService _service;

        public StorageController(ILogger<StorageService> logger, LaptopDbContext context, IAllRepository<Storage> repo)
        {
            _service = new StorageService(repo, logger, context);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllInvoiceDetail(int page = 1, int pageSize = 5)
        {
            return Ok( await _service.GetAll(page, pageSize));
        }
        [HttpPut("do-update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStorage USt)
        {
            return Ok(await _service.Update(id, USt));
        }
        [HttpGet("get-by-{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok(await _service.GetById(id));
        }
        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] CreateStorage Cst)
        {
            return Ok (await _service.Create(Cst));
        }
        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            return Ok ( await _service.delete(id));
        }
    }
}
