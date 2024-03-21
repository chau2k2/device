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

            //var laptop = await _context.laptops.Include(l => l.Storage).FirstOrDefaultAsync(l => l.Name == invoiceDetail.ProductName);

            if (invoice == null)
            {
                return new BaseResponse<InvoiceDetailModel>
                {
                    Success = false,
                    Message = "Hóa đơn không tồn tại!!!",
                    ErrorCode = ErrorCode.NotFound
                };
            }

            //if (laptop == null || laptop.IsDelete == true)
            //{
            //    return new BaseResponse<InvoiceDetailModel>
            //    {
            //        Success = false,
            //        Message = "Laptop không tồn tại!!!",
            //        ErrorCode = ErrorCode.NotFound
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

            //var existLaptop = await _context.InvoicesDetail.FirstOrDefaultAsync(d => d.InvoiceId == invoiceDetail.InvoiceId & d.LaptopId == invoiceDetail.LaptopId);

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
