using device.Data;
using device.DTO.HDonDetail;
using device.IRepository;
using device.Models;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly InvoiceDetailService _service;
        public InvoiceDetailController(IAllRepository<InvoiceDetail> repo, LaptopDbContext context, ILogger<InvoiceDetailService> logger)
        {
            _service = new InvoiceDetailService(repo, logger, context);
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAllInvoiceDetail(page, pageSize));
        }

        [HttpGet("get_by_invoiceNum")]
        public async Task<IActionResult> FindByInvoiceNum(string InvoiceNum)
        {
            return Ok(await _service.findInvoiceDetailByINumber(InvoiceNum));
        }
        [HttpPost("do_create")]
        public async Task<IActionResult> CreateInvoiceDetail(CreateInvoiceDetail CID)
        {
            return Ok ( await _service.CreateInvoiceDetail(CID));
        }

        [HttpDelete("do_delete")]
        public async Task<IActionResult> DeleteInvoiceDetail(int id)
        {
            return Ok ( await _service.Delete(id));
        }
    }
}
