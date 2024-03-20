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
                        InvoiceNumber = invoiceDetail.invoices.InvoiceNumber,
                        Quantity = invoiceDetail.Quantity,
                        Price = invoiceDetail.Price,
                        IsDelete = invoiceDetail.IsDelete
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

        public async Task<ActionResult<BaseResponse<InvoiceDetail>>> CreateInvoiceDetail(InvoiceDetailModel CID)
        {
            try
            {
                int maxId = await _context.InvoicesDetail.MaxAsync(d => (int?)d.Id) ?? 0;
                int nextId = maxId + 1;

                InvoiceDetail detail = new InvoiceDetail()
                {
                    Id = nextId,
                    ///ProductType = ProductType.
                    InvoiceId = CID.InvoiceId,
                    Quantity = CID.Quantity
                };

                //var laptop = await _context.laptops.Include(l => l.Storage).FirstOrDefaultAsync(l => l.Id == CID.LaptopId);

                //if (laptop != null || laptop.inventory >= CID.Quantity) 
                //{ 
                //    detail.Price = laptop.SoldPrice;

                //    var storage = await _context.storages.FirstOrDefaultAsync( s => s.LaptopId == CID.LaptopId);

                //    if (storage != null)
                //    {
                //        laptop.Storage.SoldNumber = laptop.Storage.SoldNumber + CID.Quantity;

                //        laptop.inventory = laptop.inventory - CID.Quantity;
                //    }

                //    var laptopValue = detail.Quantity * detail.Price;

                //    var invoice = await _context.invoices.FirstOrDefaultAsync(i => i.Id == CID.InvoiceId);

                //}

                var validate = await _validate.RegexInvoice(CID);

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
