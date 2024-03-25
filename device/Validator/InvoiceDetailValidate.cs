using device.Cons;
using device.Data;
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

        public async Task<BaseResponse<InvoiceDetailModel>> RegexInvoice ( InvoiceDetailModel invoiceDetail)
        {
            var invoice = await _context.invoices.FirstOrDefaultAsync(i => i.Id == invoiceDetail.InvoiceId);

            switch (invoiceDetail.ProductType)
            {
                case Entity.EProductType.Laptop:

                    var laptop = await _context.laptops.FirstOrDefaultAsync(l => l.Id == invoiceDetail.ProductId);

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

                    var pc = await _context.PrivateComputer.FirstOrDefaultAsync(p => p.Id == invoiceDetail.ProductId);

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

                    var ram = await _context.ram.FirstOrDefaultAsync(r => r.Id == invoiceDetail.ProductId);

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

                    var vga = await _context.vgas.FirstOrDefaultAsync(v => v.Id == invoiceDetail.ProductId);

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

                    var monitor = await _context.monitors.FirstOrDefaultAsync(m => m.Id == invoiceDetail.ProductId);

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

            var existLaptop = await _context.InvoicesDetail.FirstOrDefaultAsync(d => d.InvoiceId == invoiceDetail.InvoiceId & d.IsDelete == false);

            if (existLaptop != null)
            {
                return new BaseResponse<InvoiceDetailModel>
                {
                    Success = false,
                    Message = "Hóa đơn đã chứa laptop này",
                    ErrorCode = ErrorCode.Error
                };
            }
            if (invoice == null)
            {
                return new BaseResponse<InvoiceDetailModel>
                {
                    Success = false,
                    Message = "Hóa đơn không tồn tại!!!",
                    ErrorCode = ErrorCode.NotFound
                };
            }

            if (invoiceDetail.Quantity < 0 || invoiceDetail.Quantity > Constants.MAX_QUANTITY)
            {
                return new BaseResponse<InvoiceDetailModel>
                {
                    Success = false,
                    Message = $"Số lượng Laptop phải là số dương và nhỏ hơn {Constants.MAX_QUANTITY}"
                };
            }

            var storage = await _context.storages.FirstOrDefaultAsync(s => s.ProductType == invoiceDetail.ProductType & s.ProductId == invoiceDetail.ProductId);

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
