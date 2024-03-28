using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;
using device.Response;
using device.Models;
using device.Validator;

namespace device.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IAllRepository<Invoice> _repo;
        private readonly LaptopDbContext _context;
        private readonly InvoiceDetailValidate _validate;
        private readonly ProductService _productService;

        public InvoiceService(IAllRepository<Invoice> repo, LaptopDbContext context)
        {
            _repo = repo;
            _context = context;
            _validate = new InvoiceDetailValidate(context);
            _productService = new ProductService(context);
        }
        public async Task<TPaging<InvoiceResponse>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<Invoice>().CountAsync(i => i.IsDelete == false);

                var invoices = await _context.Set<Invoice>()
                    .Include(i => i.invoiceDetail)!
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
                        IsDelete = invoice.IsDelete,
                        InvoiceDetail = invoice.invoiceDetail!
                            .Where(i => i.IsDelete == false)
                            .Select(detail => new InvoiceDetailResponse
                            {
                                Id = detail.Id,
                                ProductType = detail.ProductType,
                                ProductId = detail.ProductId,
                                InvoiceNumber = detail.invoices!.InvoiceNumber,
                                Quantity = detail.Quantity,
                                Price = detail.Price,
                                InvoiceId = invoice.Id
                            }).ToList()
                    });
                }

                return new TPaging<InvoiceResponse>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = invoiceResponses,
                    Message = "Successfull!!!"
                };
            }
            catch (Exception ex)
            {
                return new TPaging<InvoiceResponse>
                {
                    Message = ex.Message,
                    Error = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<InvoiceResponse>>> GetById(int id)
        {
            try
            {
                var invoice = await _context.Set<Invoice>()
                    .Include(i => i.invoiceDetail)!
                    .Where(i => i.IsDelete == false)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (invoice == null)
                {
                    return new BaseResponse<InvoiceResponse>
                    {
                        Success = false,
                        Message = "NotFound!!!",
                        ErrorCode = ErrorCode.Error
                    };
                }

                InvoiceResponse invoiceResponse = new InvoiceResponse()
                {
                    Id = invoice.Id,
                    InvoiceNumber = invoice.InvoiceNumber,
                    DateInvoice = invoice.DateInvoice,
                    IsDelete = invoice.IsDelete,
                    InvoiceDetail = invoice.invoiceDetail!
                        .Where(i => i.IsDelete == false)
                        .Select(detail => new InvoiceDetailResponse
                        {
                            Id = detail.Id,
                            ProductType = detail.ProductType,
                            ProductId = detail.ProductId,
                            InvoiceNumber = detail.invoices!.InvoiceNumber,
                            Quantity = detail.Quantity,
                            Price = detail.Price,
                            InvoiceId = invoice.Id
                        }).ToList()
                };

                return new BaseResponse<InvoiceResponse>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = invoiceResponse,
                    ErrorCode = ErrorCode.None
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<InvoiceResponse>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Invoice>>> Create(InvoiceModel model)
        {
            try
            {
                int maxId = await _context.invoices.MaxAsync(e => (int?)e.Id) ?? 0;

                int nextId = maxId + 1;

                InvoiceDetail invoiceDetail = new InvoiceDetail()
                {
                    ProductId = model.Details.ProductId,
                    ProductType = model.Details.ProductType,
                    Quantity = model.Details.Quantity
                };

                Invoice invoice = new Invoice()
                {
                    Id = nextId,
                    DateInvoice = DateTime.Now,
                    IsDelete = model.IsDelete,
                    invoiceDetail = new List <InvoiceDetail>()
                    {
                        invoiceDetail
                    }
                };

                invoice.InvoiceNumber = $"IV{invoice.Id:D4}";

                var invoiceDetailq = await _context.InvoicesDetail.FirstOrDefaultAsync(e => e.InvoiceId == model.Id);

                var storage = await _context.storages.FirstOrDefaultAsync(s => s.ProductType == model.Details.ProductType && s.ProductId == model.Details.ProductId);

                if (storage != null & invoiceDetail != null)
                {
                    storage!.SoldNumber = storage.SoldNumber += model.Details.Quantity;

                    storage!.inventory = storage.inventory -= model.Details.Quantity;
                }

                await _productService.ProductTypePrice(invoiceDetail!.Price, model.Details.ProductId, model.Details.ProductType);

                var validate = await _validate.RegexInvoice(model.Details);

                if (!validate.Success)
                {
                    return new BaseResponse<Invoice>
                    {
                        Success = false,
                        Message = validate.Message,
                        ErrorCode = validate.ErrorCode
                    };
                }

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
                return new BaseResponse<Invoice>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Invoice>>> Delete(int id)
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

                var invoiceDetail = await _context.InvoicesDetail.FirstOrDefaultAsync( d => d.InvoiceId == id && d.IsDelete == false);

                if (invoiceDetail != null)
                {
                    invoiceDetail.IsDelete = true;
                }

                var del = await _repo.UpdateOneAsyns(invoice);

                return new BaseResponse<Invoice>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = del
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Invoice>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }

    }
}
