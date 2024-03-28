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
        private readonly ProductService _productService;

        public InvoiceDetailService(IAllRepository<InvoiceDetail> repo, LaptopDbContext context)
        {
            _repo = repo;
            _context = context;
            _validate = new InvoiceDetailValidate(context);
            _productService = new ProductService(context);
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
                        ProductId = invoiceDetail.ProductId!,
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
                    ProductId = model.ProductId
                };

                var storage = await _context.storages.FirstOrDefaultAsync(s => s.ProductType == model.ProductType && s.ProductId == model.ProductId);

                if (storage != null)
                {
                    storage!.SoldNumber += detail.Quantity;

                    storage!.inventory -= detail.Quantity;
                }
                
                decimal price = await _productService.ProductTypePrice(detail.Price, model.ProductId, model.ProductType);
                detail.Price = price;

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

                var storage = await _context.storages.FirstOrDefaultAsync(s => s.ProductType == invoiceDetail.ProductType && s.ProductId == invoiceDetail.ProductId);

                storage!.SoldNumber = storage.SoldNumber - invoiceDetail.Quantity;

                storage!.inventory = storage.ImportNumber - storage.SoldNumber;

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

        public async Task<ActionResult<BaseResponse<InvoiceDetail>>> Update(int id, InvoiceDetailModel model)
        {
            try
            {
                var invoiceDetail = await _repo.GetAsyncById(id);

                if (invoiceDetail == null || invoiceDetail!.IsDelete)
                {
                    return new BaseResponse<InvoiceDetail>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }

                // Cập nhật các thuộc tính của invoiceDetail với giá trị mới
                invoiceDetail.ProductType = model.ProductType;
                invoiceDetail.ProductId = model.ProductId;
                invoiceDetail.InvoiceId = model.InvoiceId;
                invoiceDetail.Quantity = model.Quantity;
                invoiceDetail.IsDelete = model.IsDelete;

                // Lấy giá mới từ ProductService và gán cho invoiceDetail
                decimal price = await _productService.ProductTypePrice(invoiceDetail.Price, model.ProductId, model.ProductType);
                invoiceDetail.Price = price;

                var storage = await _context.storages.FirstOrDefaultAsync(s => s.ProductType == invoiceDetail.ProductType && s.ProductId == invoiceDetail.ProductId);

                //trường hợp chỉ update số lượng- giữ nguyên sản phẩm
                if (invoiceDetail.ProductType == model.ProductType && invoiceDetail.ProductId == model.ProductId && invoiceDetail.Quantity != model.Quantity)
                {
                    var discrepancy = invoiceDetail.Quantity - model.Quantity;

                    if (storage != null)
                    {
                        storage.inventory -= discrepancy;
                        storage.SoldNumber += discrepancy;
                    }
                }
                else //trường hợp update sản phẩm
                {
                    var storageOld = await _context.storages.FirstOrDefaultAsync(o => o.ProductType == invoiceDetail.ProductType && o.ProductId == invoiceDetail.ProductId);

                    if (storageOld != null)
                    {
                        storageOld.inventory += invoiceDetail.Quantity;
                        storageOld.SoldNumber -= invoiceDetail.Quantity;
                    }

                    var storageNew = await _context.storages.FirstOrDefaultAsync(o => o.ProductType == model.ProductType && o.ProductId == model.ProductId);

                    if (storageNew != null)
                    {
                        storageNew.SoldNumber += model.Quantity;
                        storageNew.inventory = storageNew.ImportNumber - model.Quantity;
                    }
                }

                var result = await _repo.UpdateOneAsyns(invoiceDetail);

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
    }
}
