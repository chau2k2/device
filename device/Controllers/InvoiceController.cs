using device.Data;
using device.DTO.HoaDon;
using device.IRepository;
using device.Models;
using device.Validation;
using device.Validation.CheckName;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Linq;
using System.Threading;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        protected readonly IAllRepository<Invoice> _repo;
        protected readonly IAllRepository<InvoiceDetail> _repoDetail;
        private readonly LaptopDbContext _context;
        private readonly InvoiceValidate _invoiceValidate;
        private static int _count = 0; // dem hoa don
        public HoaDonController(IAllRepository<Invoice> repo, IAllRepository<InvoiceDetail> repoDetail,LaptopDbContext context)
        {
            _repo = repo;
            _repoDetail = repoDetail;
            _context = context;
            _invoiceValidate = new InvoiceValidate();
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
            string formatteDate = civ.DateInvoice.ToString("yyyy-MM-dd HH:mm:ss");
            double total = 0;

            Invoice invoice = new Invoice()
            {
                Id = civ.Id,
                InvoiceNumber = "IV" + (_count.ToString().PadLeft(5, '0')),
                DateInvoice = civ.DateInvoice,
                TotalInvoice = civ.TotalInvoice
            };
            try
            {
                var validate = _invoiceValidate.Validate(invoice);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _repo.AddOneAsync(invoice);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create invoice not successfull!!!");
            }

        }
    }
}
