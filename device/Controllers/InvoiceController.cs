using Microsoft.AspNetCore.Mvc;
using device.IServices;
using device.Models;

namespace device.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _service;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IInvoiceService service, ILogger<InvoiceController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> FindById(int id)
        {
            return Ok ( await _service.GetById(id));
        }

        [HttpPost("do-create")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceModel model)
        {
            return Ok ( await _service.Create(model));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            return Ok ( await _service.Delete(id));
        }
    }
}
