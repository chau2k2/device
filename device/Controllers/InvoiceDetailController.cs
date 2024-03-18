using Microsoft.AspNetCore.Mvc;
using device.Response;
using device.IServices;
using device.Models;

namespace device.Controllers
{
    [Route("api/invoice-detail")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailService _service;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceDetailController(IInvoiceDetailService service, ILogger<InvoiceController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAllInvoiceDetail(page, pageSize));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> FindByInvoiceNum(int id)
        {
            return Ok(await _service.findById(id));
        }
        [HttpPost("do-create")]
        public async Task<IActionResult> CreateInvoiceDetail(InvoiceDetailModel CID)
        {
            return Ok ( await _service.CreateInvoiceDetail(CID));
        }

        [HttpDelete("do-delete")]
        public async Task<IActionResult> DeleteInvoiceDetail(int id)
        {
            return Ok ( await _service.Delete(id));
        }
    }
}
