using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;

namespace device.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ILogger<InvoiceService> _logger;
        private readonly IAllRepository<Invoice> _repo;
        private readonly LaptopDbContext _context;

        public InvoiceService(IAllRepository<Invoice> repo, ILogger<InvoiceService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repo = repo;
            _context = context;
        }
        public async Task<IEnumerable<Invoice>> GetAll(int page, int pageSize)
        {
            try
            {
                var result = await _repo.GetAllAsync(page, pageSize);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Invoice>> GetByInvoiceNum(string invoiceNum)
        {
            var findNum = await _context.invoices.FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNum);
            if (findNum == null)
            {
                return new NotFoundResult();
            }
            return findNum;
        }
        public async Task<ActionResult<Invoice>> Create(InvoiceResponse CrI)
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
                DateInvoice = CrI.DateInvoice,
                TotalQuantity = totalQuantity,
                TotalPrice = totalPrice
            };

            invoice.InvoiceNumber = $"IV{invoice.Id:D4}";

            try
            {
                var result = await _repo.AddOneAsync(invoice);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Invoice>> Update(int id, InvoiceResponse UpI)
        {
            Invoice invoice = new Invoice()
            {
                Id = id,
                DateInvoice = UpI.DateInvoice
            };

            invoice.InvoiceNumber = $"IV{invoice.Id:D4}";

            try
            {
                var result = await _repo.UpdateOneAsyns(invoice);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Invoice>> delete(int id)
        {
            try
            {
                var invoice = await _repo.GetAsyncById(id);
                if (invoice == null)
                {
                    throw new Exception("Not found invoice");
                }
                invoice.IsDelete = true;
                var del = await _repo.DeleteOneAsync(invoice);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("Can't delete this invoice");
            }
        }
    }
}
