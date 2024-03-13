using Microsoft.AspNetCore.Mvc;
using device.ModelResponse;
using device.IServices;

namespace device.Controllers
{
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _service;
        private readonly ILogger<StorageController> _logger;

        public StorageController(IStorageService storageService, ILogger<StorageController> logger)
        {
            _service = storageService;
            _logger = logger;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllInvoiceDetail(int page = 1, int pageSize = 5)
        {
            return Ok( await _service.GetAll(page, pageSize));
        }
        [HttpPut("do-update")]
        public async Task<IActionResult> Update(int id, [FromBody] StorageResponse USt)
        {
            return Ok(await _service.Update(id, USt));
        }
        [HttpGet("get-by-{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok(await _service.GetById(id));
        }
        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] StorageResponse Cst)
        {
            return Ok (await _service.Create(Cst));
        }
        [HttpDelete("delete-vga")]
        public async Task<IActionResult> delete(int id)
        {
            return Ok ( await _service.delete(id));
        }
    }
}
