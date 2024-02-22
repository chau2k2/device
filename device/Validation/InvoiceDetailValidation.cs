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
            RuleFor(detail => detail.IdLaptop).Must(l => IsValueIdLaptop(l)).WithMessage("Not found Idlaptop!!!");
            RuleFor(detail => detail.IdLaptop).Must(lap => IsValueStorage(lap)).WithMessage($"this laptop does not have storage data!!!");
            RuleFor(detail => detail.IdInvoice).Must(l => IsValueIdInvoice(l)).WithMessage("Not found IdInvoice!!!");  
        }
        private bool IsValueIdLaptop (int id)
        {
            return _context.laptops.Any (x => x.Id == id);
        } 
        private bool IsValueIdInvoice (int id)
        {
            return _context.invoices.Any (x => x.Id == id);
        }
        private bool IsValueStorage (int idLap)
        {
            var laptop = _context.laptops.FirstOrDefault(x => x.Id == idLap);
            if (laptop != null)
            {
                var idLapDetail = _context.laptopsDetail
                    .Where(d => d.idLaptop == idLap)
                    .Select( d => d.Id)
                    .FirstOrDefault();
                return _context.storages.Any(s => s.idDetail == idLapDetail);
            }
            return false;
        }
    }
}
