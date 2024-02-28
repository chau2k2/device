using device.Data;
using device.DTO.HoaDon;
using device.IRepository;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public InvoiceController(IAllRepository<Invoice> repo, IAllRepository<InvoiceDetail> repoDetail, LaptopDbContext context)
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
        public async Task<IActionResult> FindByInvoiceNum(string invoiceNum)
        {
            var findNum = _context.invoices.FirstOrDefault(i => i.InvoiceNumber == invoiceNum);
            if (findNum == null)
            {
                return NotFound(new { Message = "Invoice is not exist" });
            }
            return Ok(findNum);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoice civ)
        {
            int maxId = await _context.invoices.MaxAsync(e => (int?)e.Id) ?? 0;
            int nextId = maxId + 1;

            decimal totalPrice = 0;
            int totalQuantity = 0;

            List<InvoiceDetail> listdetail = new List<InvoiceDetail>();
            foreach (var detail in listdetail)
            {
                totalPrice += detail.Price * detail.Quantity;
                totalQuantity += detail.Quantity;
            }

            Invoice invoice = new Invoice()
            {
                Id = nextId,
                DateInvoice = civ.DateInvoice,
                TotalQuantity = totalQuantity,
                TotalPrice = totalPrice
            };

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

        [HttpPut]
        public async Task<IActionResult> UpdateInvoice(int id, UpdateInvoice upI)
        {
            Invoice invoice = new Invoice()
            {
                Id = id,
                DateInvoice = upI.DateInvoice
            };

            invoice.InvoiceNumber = $"IV{invoice.Id:D4}";

            try
            {
                var validate = _invoiceValidate.Validate(invoice);
                if (!validate.IsValid)
                {
                    return BadRequest(validate.Errors);
                }
                var result = await _repo.UpdateOneAsyns(invoice);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update invoice not successfull!!!");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            try
            {
                var findId = await _repo.GetAsyncById(id);
                if (findId == null) { return NotFound(); }
                var val = _invoiceValidate.Validate(findId);
                if (!val.IsValid) { return BadRequest(val.Errors); }
                return Ok(await _repo.DeleteOneAsync(findId));
            }
            catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException postgresException)
            {
                string message = postgresException.MessageText;
                string constraintName = postgresException.ConstraintName;

                return BadRequest($"Error: {message}. Constraint: {constraintName}");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }
    }
}
