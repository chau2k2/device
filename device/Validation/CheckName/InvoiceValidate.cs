using device.Models;
using FluentValidation;

namespace device.Validation.CheckName
{
    public class InvoiceValidate : AbstractValidator<Invoice>
    {
        public InvoiceValidate() 
        {
            RuleFor(invoice => invoice.DateInvoice).NotEmpty().WithMessage("Datatime to create invoice is required")
                .Must(BeValueDate).WithMessage("The invoice date must be less than or equal to the current time and not more than 10 years before");
        }
        private bool BeValueDate (DateTime date)
        {
            DateTime currentTime = DateTime.Now;
            DateTime tenYearAgo = currentTime.AddYears(-10);
            return date <= currentTime && date >= tenYearAgo;
        }
    }
}
