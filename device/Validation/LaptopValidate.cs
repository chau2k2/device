using device.Cons;
using device.Models;
using device.Validation.CheckName;
using FluentValidation;

namespace device.Validation
{
    public class LaptopValidate : AbstractValidator<Laptop>
    {
        private readonly AllNameRepeat _nameRepeat;
        public LaptopValidate()
        {
            _nameRepeat = new AllNameRepeat();
            RuleFor(lap => lap.Name).NotNull().WithMessage("Name is not null")
                .Must(name => _nameRepeat.IsValueName(name)).WithMessage("dont spam the name");
            RuleFor(lap => lap.Name).MaximumLength(Constants.MAX_LENGTH_NAME).WithMessage($"Length's Name is not greate than {Constants.MAX_LENGTH_NAME}");
            RuleFor(lap => lap.CostPrice).InclusiveBetween(0, Constants.MAX_PRICE).WithMessage($"price must be between 0 and {Constants.MAX_PRICE}");
            RuleFor(lap => lap.CostPrice).GreaterThan(lap => lap.CostPrice).WithMessage("gia ban phai lon hon gia nhap");
        }
    }
}
