using device.Cons;
using device.Data;
using device.Models;
using FluentValidation;
using System.Reflection.Metadata;

namespace device.Validation
{
    public class InvoiceDetailValidation :AbstractValidator<InvoiceDetail>
    {
        private readonly LaptopDbContext _context;
        private readonly Storage _storage;
        public InvoiceDetailValidation(LaptopDbContext context) 
        {
            _context = context;
            _storage = new Storage();
            RuleFor(detail => detail.IdLaptop).Must(l => IsValueIdLaptop(l)).WithMessage("Invalid Idlaptop!!!");
            RuleFor(detail => detail.IdInvoice).Must(l => IsValueIdInvoice(l)).WithMessage("Invalid IdInvoice!!!");
            RuleFor(detail => detail.Quantity).InclusiveBetween(0, _storage.InserNumber).WithMessage($"Quantity must be < {_storage.InserNumber}");
        }
        private bool IsValueIdLaptop (int id)
        {
            return _context.laptops.Any (x => x.Id == id);
        } 
        private bool IsValueIdInvoice (int id)
        {
            return _context.invoices.Any (x => x.Id == id);
        }

    }
}
