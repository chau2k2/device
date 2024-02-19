using device.Cons;
using device.Models;
using FluentValidation;

namespace device.Validation
{
    public class StorageValidate: AbstractValidator<Storage>
    {
        public StorageValidate()
        {
            RuleFor(khohang => khohang.SaleNumber).NotEmpty().WithMessage("The quantity sold must not be null.");
            RuleFor(khohang => khohang.InserNumber).NotEmpty().WithMessage("The quantity input must not be null.");
            RuleFor(khohang => khohang.InserNumber).InclusiveBetween(0, Constants.MAX_QUANTITY).WithMessage($"The quantity input must be between 0 and {Constants.MAX_QUANTITY}");
            RuleFor(khohang => khohang.SaleNumber).InclusiveBetween(0, Constants.MAX_QUANTITY).WithMessage($"The quantity sold must be between 0 and {Constants.MAX_QUANTITY}");
        }
    }
}
