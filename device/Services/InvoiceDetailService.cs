using device.Data;
using device.DTO.HDonDetail;
using device.IRepository;
using device.Models;
using device.Response;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace device.Services
{
    public class InvoiceDetailService
    {
        private readonly ILogger<InvoiceDetailService> _logger;
        private readonly IAllRepository<InvoiceDetail> _repo;
        private readonly LaptopDbContext _context;
        private readonly InvoiceDetailValidation _validate;

        public InvoiceDetailService(IAllRepository<InvoiceDetail> repo, ILogger<InvoiceDetailService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repo = repo;
            _context = context;
            _validate = new InvoiceDetailValidation(context);
        }
        public async Task<IEnumerable<InvoiceDetailResponse>> GetAllInvoiceDetail(int page = 1, int pageSize = 5)
        {
            try
            {
                var result = await _context.Set<InvoiceDetail>()!
                    .Include(l => l.Laptop)
                    .Include(i => i.invoices)
                    .Take(page).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<InvoiceDetailResponse> InvoiceDetailResponse = new List<InvoiceDetailResponse>();

                foreach (var invoiceDetail in result)
                {
                    InvoiceDetailResponse.Add(new InvoiceDetailResponse()
                    {
                        Id = invoiceDetail.Id,
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

        public async Task<ActionResult<InvoiceDetail>> CreateInvoiceDetail(CreateInvoiceDetail CID)
        {
            try
            {
                int maxId = await _context.InvoicesDetail.MaxAsync(d => (int?)d.Id) ?? 0;
            int nextId = maxId + 1;

            InvoiceDetail detail = new InvoiceDetail()
            {
                Id = nextId,
                LaptopId = CID.IdLaptop,
                InvoiceId = CID.IdInvoice,
                Quantity = CID.Quantity
            };

            _context.invoices.FirstOrDefault(i => i.Id == CID.IdInvoice);

            var laptop = _context.laptops.FirstOrDefault(l => l.Id == CID.IdLaptop);
            if (laptop != null) { detail.Price = laptop.SoldPrice; }

                var val = _validate.Validate(detail);
                if (!val.IsValid) 
                { 
                    throw new Exception(string.Join(", ", val.Errors)); 
                }
                var result = await _repo.AddOneAsync(detail); 
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult<InvoiceDetailResponse>> UpdateInvoice(int id, UpdateInvoiceDetail UID)
        {
            InvoiceDetail detail = new InvoiceDetail()
            {
                Id = id,
                InvoiceId = UID.IdInvoice,
                LaptopId = UID.IdLaptop,
                Quantity = UID.Quantity
            };

            if (detail.IdLaptop != UID.IdLaptop)
            {
                var laptop = _dbcontext.laptops.FirstOrDefault(l => l.Id == detail.IdLaptop);
                if (laptop != null) { detail.Price = laptop.SoldPrice; }
            }
            else
            {
                detail.Price = detail.Price;
            }

            try
            {
                var val = _validate.Validate(detail);
                if (!val.IsValid) { return BadRequest(val.Errors); }
                var result = await _repo.UpdateOneAsyns(detail); return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Invoice Detail not successfull!!!");
            }
        }
    }
}
