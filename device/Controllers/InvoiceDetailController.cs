using device.Data;
using device.DTO.HDonDetail;
using device.IRepository;
using device.Models;
using device.Services;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            return Ok(await _service.GetAllInvoiceDetail(page, pageSize));
        }

        //[HttpGet("GetByInvoiceNum/Notdone")]
        //public async Task<IActionResult> FindByInvoiceNum(string InvoiceNum)
        //{
        //    return Ok();
        //}
        [HttpPost]
        public async Task<IActionResult> CreateInvoiceDetail(CreateInvoiceDetail CID)
        {
            return Ok ( await _service.CreateInvoiceDetail(CID));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInvoiceDetail(int id)
        {
            return Ok ( await _service.Delete(id));
        }
    }
}
