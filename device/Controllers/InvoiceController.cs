using device.Data;
using device.DTO.HoaDon;
using device.IRepository;
using device.Models;
using device.Validation;
using device.Validation.CheckName;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;

namespace device.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        protected readonly IAllRepository<Invoice> _repo;
        protected readonly IAllRepository<InvoiceDetail> _repoDetail;
        private readonly LaptopDbContext _context;
        private readonly InvoiceValidate _invoiceValidate;
        private readonly InvoiceDetail _detail;

        public InvoiceController(IAllRepository<Invoice> repo, IAllRepository<InvoiceDetail> repoDetail,LaptopDbContext context)
        {
            _repo = repo;
            _repoDetail = repoDetail;
            _context = context;
            _invoiceValidate = new InvoiceValidate();
            _detail = new InvoiceDetail();
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
        public async Task<IActionResult> CreateInvoice ([FromBody]CreateInvoice civ)
        {
            int maxId = await _context.invoices.MaxAsync(e => (int?)e.Id) ?? 0;
            int nextId = maxId + 1;
            double totalPrice = 0;
            int totalQuantity = 0;
            List<InvoiceDetail> listdetail = new List<InvoiceDetail>();
            foreach (var detail in listdetail)
            {
                totalPrice += detail.Price * detail.Quantity;
                totalQuantity += detail.Quantity;
            }
            Invoice invoice = new Invoice()
            {
                DateInvoice = civ.DateInvoice,
                TotalQuantity = totalQuantity,
                TotalPrice = totalPrice
            };
            invoice.Id = nextId;
            invoice.InvoiceNumber = $"IV{invoice.Id:D4}";
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
        [HttpDelete]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var findId = await _repo.GetAsyncById(id);
            if (findId == null){ return NotFound(); }
            return Ok(await _repo.DeleteOneAsync(findId));
        }
    }
}
