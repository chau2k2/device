using device.Cons;
using device.Data;
using device.Entity;
using device.Models;
using device.Response;
using Microsoft.EntityFrameworkCore;

namespace device.Validator
{
    public class InvoiceDetailValidate
    {
        private readonly LaptopDbContext _context;

        public InvoiceDetailValidate(LaptopDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<InvoiceDetailModel>> RegexInvoice ( InvoiceDetailModel model)
        {
            var invoice = await _context.invoices.FirstOrDefaultAsync(i => i.Id == model.InvoiceId);

            switch (model.ProductType)
            {
                case Entity.EProductType.Laptop:

                    var laptop = await _context.laptops.FirstOrDefaultAsync(l => l.Id == model.ProductId);

                    if (laptop == null || laptop.IsDelete == true)
                    {
                        return new BaseResponse<InvoiceDetailModel>
                        {
                            Success = false,
                            Message = "Laptop không tồn tại!!!",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;

                case Entity.EProductType.PrivateComputer:

                    var pc = await _context.PrivateComputer.FirstOrDefaultAsync(p => p.Id == model.ProductId);

                    if (pc == null || pc.IsDelete == true)
                    {
                        return new BaseResponse<InvoiceDetailModel>
                        {
                            Success = false,
                            Message = "Pc không tồn tại",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;

                case Entity.EProductType.Ram:

                    var ram = await _context.ram.FirstOrDefaultAsync(r => r.Id == model.ProductId);

                    if (ram == null || ram.IsDelete == true)
                    {
                        return new BaseResponse<InvoiceDetailModel>
                        {
                            Success = false,
                            Message = "Ram không tồn tại",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;

                case Entity.EProductType.Vga:

                    var vga = await _context.vgas.FirstOrDefaultAsync(v => v.Id == model.ProductId);

                    if (vga == null || vga.IsDelete == true)
                    {
                        return new BaseResponse<InvoiceDetailModel>
                        {
                            Success = false,
                            Message = "Vga không tồn tại",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;

                case Entity.EProductType.Monitor:

                    var monitor = await _context.monitors.FirstOrDefaultAsync(m => m.Id == model.ProductId);

                    if (monitor == null || monitor.IsDelete == true)
                    {
                        return new BaseResponse<InvoiceDetailModel>
                        {
                            Success = false,
                            Message = "Màn hình không tồn tại",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;

                default:
                    return new BaseResponse<InvoiceDetailModel>
                    {
                        Success = true
                    };
            }
            if (invoice != null)
            {
                var invoiceDetail = await _context.InvoicesDetail.FirstOrDefaultAsync(d => d.InvoiceId == invoice.Id && d.ProductType == model.ProductType && d.ProductId == model.ProductId);
                if (invoiceDetail != null)
                {
                    return new BaseResponse<InvoiceDetailModel>
                    {
                        Success = false,
                        Message = "Hóa đơn đã tồn tại sản phẩm này!",
                        ErrorCode = ErrorCode.Error
                    };
                }
            }  
            
            //if (invoice == null)
            //{
            //    return new BaseResponse<InvoiceDetailModel>
            //    {
            //        Success = false,
            //        Message = "Hóa đơn không tồn tại!!!",
            //        ErrorCode = ErrorCode.NotFound
            //    };
            //}

            if (model.Quantity < 0 || model.Quantity > Constants.MAX_QUANTITY)
            {
                return new BaseResponse<InvoiceDetailModel>
                {
                    Success = false,
                    Message = $"Số lượng Laptop phải là số dương và nhỏ hơn {Constants.MAX_QUANTITY}"
                };
            }

            var storage = await _context.storages.FirstOrDefaultAsync(s => s.ProductType == model.ProductType & s.ProductId == model.ProductId);

            if (storage == null)
            {
                return new BaseResponse<InvoiceDetailModel>
                {
                    Success = false,
                    Message = "Sản phẩm này chưa được cập nhật số lượng kho",
                    ErrorCode = ErrorCode.Error
                };
            }
            
            return new BaseResponse<InvoiceDetailModel>
            {
                Success = true
            };
        }
    }
}
