using device.DTO.HoaDon;
using device.IRepository;
using device.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Linq;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        protected readonly IAllRepository<Invoice> _repo;
        protected readonly IAllRepository<InvoiceDetail> _repoDetail;
        private static int _count = 0; // dem hoa don
        public HoaDonController(IAllRepository<Invoice> repo, IAllRepository<InvoiceDetail> repoDetail)
        {
            _repo = repo;
            _repoDetail = repoDetail;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 5)
        {
            var result = await _repo.GetAllAsync(page, pageSize);
            if (result == null) { return StatusCode(StatusCodes.Status404NotFound, "Empty list"); }
            return Ok(result);
        }
        [HttpGet("GetInvoiceNum")]
        public async Task<IActionResult> FindByInvoiceNum (string invoiceNum)
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CreateInvoice (CreateInvoice civ)
        {
            _count ++;
            if (civ.DateInvoice == DateTime.MinValue)
            {
                civ.DateInvoice = DateTime.Now;
            }
            Invoice invoice = new Invoice()
            {
                Id = civ.Id,
                InvoiceNumber = "IV" + (_count.ToString().PadLeft(5, '0')),
                DateInvoice = civ.DateInvoice,
                TotalInvoice = civ.TotalInvoice
            };
            try
            {
                var result = await _repo.AddOneAsync(invoice);
                return Ok(invoice);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create invoice not successfull!!!");
            }
        }
    }
}
