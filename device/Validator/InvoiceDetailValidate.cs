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
            var invoice = _context.invoices.FirstOrDefaultAsync(i => i.Id == invoiceDetail.InvoiceId);

            //var laptop = _context.laptops.FirstOrDefaultAsync (i => i.Id == invoiceDetail.LaptopId);

            //if (invoice == null)
            //{
            //    return new BaseResponse<InvoiceDetailModel>
            //    {
            //        Success = false,
            //        Message = "Hóa đơn không tồn tại!!!",
            //        ErrorCode = ErrorCode.NotFound
            //    };
            //}

            //if (laptop == null)
            //{
            //    return new BaseResponse<InvoiceDetailModel>
            //    {
            //        Success = false,
            //        Message = "Laptop không tồn tại!!!",
            //        ErrorCode = ErrorCode.NotFound
            //    };
            //}

            //var storage = await _context.storages.FirstOrDefaultAsync( s => s.LaptopId == invoiceDetail.LaptopId);

            //if (storage == null)
            //{
            //    return new BaseResponse<InvoiceDetailModel>
            //    {
            //        Success = false,
            //        Message = "Laptop này chưa được cập nhật số lượng!!!"
            //    };
            //}

            if (invoiceDetail.Quantity < 0 || invoiceDetail.Quantity > Constants.MAX_QUANTITY)
            {
                return new BaseResponse<InvoiceDetailModel>
                {
                    Success = false,
                    Message = $"Số lượng Laptop phải là số dương và nhỏ hơn {Constants.MAX_QUANTITY}"
                };
            }

            //var existLaptop = await _context.InvoicesDetail.FirstOrDefaultAsync( d => d.InvoiceId == invoiceDetail.InvoiceId & d.LaptopId == invoiceDetail.LaptopId);

            //if (existLaptop != null)
            //{
            //    return new BaseResponse<InvoiceDetailModel>
            //    {
            //        Success = false,
            //        Message = "Hóa đơn đã chứa laptop này",
            //        ErrorCode = ErrorCode.Error
            //    };
            //}

            return new BaseResponse<InvoiceDetailModel>
            {
                Success = true
            };
        }
    }
}
