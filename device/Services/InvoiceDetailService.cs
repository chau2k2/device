using device.Data;
using device.IRepository;
using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.IServices;
using device.Models;
using device.Validator;

namespace device.Services
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly IAllRepository<InvoiceDetail> _repo;
        private readonly LaptopDbContext _context;
        private readonly InvoiceDetailValidate _validate;

        public InvoiceDetailService(IAllRepository<InvoiceDetail> repo, LaptopDbContext context)
        {
            _repo = repo;
            _context = context;
            _validate = new InvoiceDetailValidate(context);
        }
        public async Task<TPaging<InvoiceDetailResponse>> GetAllInvoiceDetail(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<InvoiceDetail>().CountAsync(I => I.IsDelete == false);  
                
                var result = await _context.Set<InvoiceDetail>()!
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
                        InvoiceId = invoiceDetail.InvoiceId,
                        InvoiceNumber = invoiceDetail.invoices?.InvoiceNumber,
                        Quantity = invoiceDetail.Quantity,
                        Price = invoiceDetail.Price,
                        IsDelete = invoiceDetail.IsDelete,
                        ProductName = invoiceDetail.NameProduct!,
                        ProductType = invoiceDetail.ProductType
                    });
                }

                return new TPaging<InvoiceDetailResponse>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = InvoiceDetailResponse
                };
            }
            catch (Exception ex)
            {
                return new TPaging<InvoiceDetailResponse>
                {
                    Message = ex.Message,
                    Error = Error.Error
                };
            }
        }

        public async Task<ActionResult<BaseResponse<InvoiceDetail>>> CreateInvoiceDetail(InvoiceDetailModel model)
        {
            try
            {
                int maxId = await _context.InvoicesDetail.MaxAsync(d => (int?)d.Id) ?? 0;
                int nextId = maxId + 1;

                InvoiceDetail detail = new InvoiceDetail()
                {
                    Id = nextId,
                    ProductType = model.ProductType,
                    InvoiceId = model.InvoiceId,
                    Quantity = model.Quantity,
                    NameProduct = model.ProductName
                };

                switch (detail.ProductType)
                {
                    case EProductType.Laptop:
                        var laptop = await _context.laptops.FirstOrDefaultAsync(l => l.Name == model.ProductName);
                        detail.Price = laptop!.SoldPrice;
                        break;
                    case EProductType.PrivateComputer:
                        var pc = await _context.PrivateComputer.FirstOrDefaultAsync(p => p.Name == model.ProductName);
                        detail.Price = pc!.SoldPrice;
                        break;
                    case EProductType.Ram:
                        var ram = await _context.ram.FirstOrDefaultAsync(r =>  r.Name == model.ProductName);
                        detail.Price = ram!.Price;
                        break;
                    case EProductType.Monitor:
                        var monitor = await _context.monitors.FirstOrDefaultAsync(m => m.Name == model.ProductName);
                        detail.Price = monitor!.Price;
                        break;
                    case EProductType.Vga:
                        var vga = await _context.vgas.FirstOrDefaultAsync( v => v.Name == model.ProductName);
                        detail.Price = vga!.Price;
                        break;

                }

                var validate = await _validate.RegexInvoice(model);

                if (!validate.Success)
                {
                    return new BaseResponse<InvoiceDetail>
                    {
                        Success = false,
                        Message = validate.Message,
                        ErrorCode = validate.ErrorCode
                    };
                }

                var result = await _repo.AddOneAsync(detail); 

                return new BaseResponse<InvoiceDetail>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<InvoiceDetail>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<InvoiceDetail>>> Delete (int id)
        {
            try
            {
                var invoiceDetail = await _repo.GetAsyncById(id);

                if (invoiceDetail == null || invoiceDetail.IsDelete == true)
                {
                    return new BaseResponse<InvoiceDetail>
                    {
                        Success = false,
                        Message = "NotFound!!!",
                        ErrorCode = ErrorCode.NotFound
                    };
                }

                invoiceDetail.IsDelete = true;
                
                var delInvoiceDetail = await _repo.UpdateOneAsyns(invoiceDetail);

                return new BaseResponse<InvoiceDetail>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = delInvoiceDetail
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<InvoiceDetail>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<InvoiceDetail>>> GetById (int id)
        {
            try
            {
                var invoiceDetail = await _repo.GetAsyncById(id);

                if (invoiceDetail == null || invoiceDetail!.IsDelete == true)
                {
                    return new BaseResponse<InvoiceDetail>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }

                return new BaseResponse<InvoiceDetail>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = invoiceDetail
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<InvoiceDetail>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error 
                };
            }
        }
    }
}
