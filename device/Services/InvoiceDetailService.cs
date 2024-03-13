using device.Data;
using device.IRepository;
using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.IServices;

namespace device.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly ILogger<InvoiceDetailService> _logger;
        private readonly IAllRepository<InvoiceDetail> _repo;
        private readonly LaptopDbContext _context;

        public InvoiceDetailService(IAllRepository<InvoiceDetail> repo, ILogger<InvoiceDetailService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repo = repo;
            _context = context;
        }
        public async Task<IEnumerable<InvoiceDetailResponse>> GetAllInvoiceDetail(int page, int pageSize)
        {
            try
            {
                var result = await _context.Set<InvoiceDetail>()!
                    .Include(l => l.Laptop)
                    .Include(i => i.invoices)
                    .Where( l => l.IsDelete == false)
                    .Take(pageSize).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<InvoiceDetailResponse> InvoiceDetailResponse = new List<InvoiceDetailResponse>();

                foreach (var invoiceDetail in result)
                {
                    InvoiceDetailResponse.Add(new InvoiceDetailResponse()
                    {
                        Id = invoiceDetail.Id,
                        LaptopId = invoiceDetail.LaptopId,
                        InvoiceId = invoiceDetail.InvoiceId,
                        LaptopName = invoiceDetail.Laptop.Name,
                        InvoiceNumber = invoiceDetail.invoices.InvoiceNumber,
                        Quantity = invoiceDetail.Quantity,
                        Price = invoiceDetail.Price
                    });
                }
                return InvoiceDetailResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult<InvoiceDetail>> CreateInvoiceDetail(InvoiceDetailResponse CID)
        {
            try
            {
                int maxId = await _context.InvoicesDetail.MaxAsync(d => (int?)d.Id) ?? 0;
                int nextId = maxId + 1;

                InvoiceDetail detail = new InvoiceDetail()
                {
                    Id = nextId,
                    LaptopId = CID.LaptopId,
                    InvoiceId = CID.InvoiceId,
                    Quantity = CID.Quantity
                };

                var laptop = _context.laptops.FirstOrDefault(l => l.Id == CID.LaptopId);
                if (laptop != null) { detail.Price = laptop.SoldPrice; }

                var result = await _repo.AddOneAsync(detail); 
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult<InvoiceDetail>> Delete (int id)
        {
            try
            {
                var invoiceDetail = await _repo.GetAsyncById(id);
                if (invoiceDetail == null)
                {
                    throw new Exception("Not found this Invoice Detail");
                }
                invoiceDetail.IsDelete = true;
                return await _repo.UpdateOneAsyns(invoiceDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<InvoiceDetail>> findInvoiceDetailByINumber (string invoiceNumber)
        {
            try
            {
                var invoiceDetail = await _context.InvoicesDetail
                    .Include(i => i.invoices)
                    .Where(i => i.invoices.InvoiceNumber == invoiceNumber)
                    .ToListAsync();
                return invoiceDetail;
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
