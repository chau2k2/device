using device.Data;
using device.Models;
using FluentValidation;

namespace device.Validation
{
    public class InvoiceDetailValidation : AbstractValidator<InvoiceDetail>
    {
        private readonly LaptopDbContext _context;
        public InvoiceDetailValidation(LaptopDbContext context) 
        {
            _context = context;       
         
        }
    }
}
