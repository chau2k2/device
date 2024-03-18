using device.Data;
using device.Entity;
using device.Models;
using device.Response;

namespace device.Validator
{
    public class InvoiceValidate
    {
        private readonly LaptopDbContext _context;
        private readonly CheckDuplicate _duplicate;

        public InvoiceValidate (LaptopDbContext context, CheckDuplicate duplicate)
        {
            _context = context;
            _duplicate = duplicate;
        }

        public async Task<BaseResponse<InvoiceModel>> RegexInvoice ( InvoiceModel invoice)
        {
            return new BaseResponse<InvoiceModel>
            {
                Success = true
            };
        }
    }
}
