using device.Data;
using device.IRepository;
using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.IServices;
using System.Xml.Schema;

namespace device.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly IAllRepository<InvoiceDetail> _repo;
        private readonly LaptopDbContext _context;

        public InvoiceDetailService(IAllRepository<InvoiceDetail> repo, LaptopDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        public async Task<TPaging<InvoiceDetailResponse>> GetAllInvoiceDetail(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<InvoiceDetail>().CountAsync(I => I.IsDelete == false);  

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
                        Price = invoiceDetail.Price,
                        IsDelete = invoiceDetail.IsDelete
                    });
                }

                return new TPaging<InvoiceDetailResponse>
                {
                    numberPage = page,
                    totalRecord = totalCount,
                    Data = InvoiceDetailResponse
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult<BaseResponse<InvoiceDetail>>> CreateInvoiceDetail(InvoiceDetailResponse CID)
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

                return new BaseResponse<InvoiceDetail>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult<BaseResponse<InvoiceDetail>>> Delete (int id)
        {
            try
            {
                var invoiceDetail = await _repo.GetAsyncById(id);

                if (invoiceDetail == null && invoiceDetail!.IsDelete == true)
                {
                    return new BaseResponse<InvoiceDetail>
                    {
                        success = false,
                        message = "NotFound!!!"
                    };
                }
                invoiceDetail.IsDelete = true;

                return new BaseResponse<InvoiceDetail>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = invoiceDetail
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult<BaseResponse<InvoiceDetail>>> findById (int id)
        {
            try
            {
                var invoiceDetail = await _repo.GetAsyncById(id);

                if (invoiceDetail == null && invoiceDetail!.IsDelete == true)
                {
                    return new BaseResponse<InvoiceDetail>
                    {
                        success = false,
                        message = "Not found!!!"
                    };
                }

                return new BaseResponse<InvoiceDetail>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = invoiceDetail
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
