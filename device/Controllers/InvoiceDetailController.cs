using Microsoft.AspNetCore.Mvc;
using device.Response;
using device.IServices;

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

        [HttpGet("get-by-invoice-number")]
        public async Task<IActionResult> FindByInvoiceNum(string InvoiceNum)
        {
            return Ok(await _service.findInvoiceDetailByINumber(InvoiceNum));
        }
        [HttpPost("do-create")]
        public async Task<IActionResult> CreateInvoiceDetail(InvoiceDetailResponse CID)
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
