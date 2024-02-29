using device.Data;
using device.DTO.HoaDon;
using device.IRepository;
using device.Models;
using device.Services;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _service;

        public InvoiceController(IAllRepository<Invoice> repo, LaptopDbContext context, ILogger<InvoiceService> logger)
        {
            _service = new InvoiceService(repo, logger, context);
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAll(page, pageSize));
        }

        [HttpGet("get_invoice_num")]
        public async Task<IActionResult> FindByInvoiceNum(string invoiceNum)
        {
            return Ok ( await _service.GetByInvoiceNum(invoiceNum));
        }

        [HttpPost("do_create")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoice civ)
        {
            return Ok ( await _service.Create(civ));
        }

        [HttpPut("do_update")]
        public async Task<IActionResult> UpdateInvoice(int id, UpdateInvoice upI)
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
