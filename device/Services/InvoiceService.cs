using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;
using device.Response;
using device.Models;

namespace device.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IAllRepository<Invoice> _repo;
        private readonly LaptopDbContext _context;

        public InvoiceService(IAllRepository<Invoice> repo,  LaptopDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        public async Task<TPaging<InvoiceResponse>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<Invoice>().CountAsync( i => i.IsDelete == false);

                var invoices = await _context.Set<Invoice>()
                    .Where(i => i.IsDelete == false)
                    .Take(pageSize).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<InvoiceResponse> invoiceResponses = new List<InvoiceResponse>();

                foreach (var invoice in invoices)
                {
                    invoiceResponses.Add(new InvoiceResponse()
                    {
                        Id = invoice.Id,
                        DateInvoice = invoice.DateInvoice,
                        InvoiceNumber = invoice.InvoiceNumber,
                        IsDelete = invoice.IsDelete
                    });
                }

                return new TPaging<InvoiceResponse>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = invoiceResponses
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Invoice>>> GetByInvoiceNum(string invoiceNum)
        {
            try
            {
                var invoice = await _context.invoices.FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNum);

                if (invoice == null)
                {
                    return new BaseResponse<Invoice>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }

                return new BaseResponse<Invoice>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = invoice
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Invoice>>> Create(InvoiceModel CrI)
        {
            try
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

                var result = await _repo.AddOneAsync(invoice);

                return new BaseResponse<Invoice>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Invoice>>> Update(int id, InvoiceModel UpI)
        {
            try
            {
                var invo = await _repo.GetAsyncById(id);

                if (invo == null || invo!.IsDelete == true)
                {
                    return new BaseResponse<Invoice>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }
                Invoice invoice = new Invoice()
                {
                    Id = id,
                    DateInvoice = UpI.DateInvoice
                };

                invoice.InvoiceNumber = $"IV{invoice.Id:D4}";

                var result = await _repo.UpdateOneAsyns(invoice);

                return new BaseResponse<Invoice>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Invoice>>> delete(int id)
        {
            try
            {
                var invoice = await _repo.GetAsyncById(id);

                if (invoice == null || invoice.IsDelete == true)
                {
                    return new BaseResponse<Invoice>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }

                invoice.IsDelete = true;

                var del = await _repo.DeleteOneAsync(invoice);

                return new BaseResponse<Invoice>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = del
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
