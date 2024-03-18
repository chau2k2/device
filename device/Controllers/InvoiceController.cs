using Microsoft.AspNetCore.Mvc;
using device.ModelResponse;
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
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpGet("get-invoice-num")]
        public async Task<IActionResult> FindByInvoiceNum(string invoiceNum)
        {
            return Ok ( await _service.GetByInvoiceNum(invoiceNum));
        }

        [HttpPost("do-create")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceModel civ)
        {
            return Ok ( await _service.Create(civ));
        }

        [HttpPut("do-update")]
        public async Task<IActionResult> UpdateInvoice(int id, InvoiceModel upI)
        {
            return Ok (await _service.Update(id, upI));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            return Ok ( await _service.delete(id));
        }
    }
}
