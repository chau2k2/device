using device.Cons;
using device.Models;
using FluentValidation;

namespace device.Validation
{
    public class LaptopValidate : AbstractValidator<Laptop>
    {
        public LaptopValidate()
        {
            RuleFor(lap => lap.Name).NotNull().WithMessage("Name is not null");
            RuleFor(lap => lap.Name).MaximumLength(Constants.MAX_LENGTH_NAME).WithMessage($"Length's Name is not greate than {Constants.MAX_LENGTH_NAME}");
            RuleFor(lap => lap.GiaVon).GreaterThan(0).WithMessage("Gia von phai lon hon 0");
            RuleFor(lap => lap.Giaban).GreaterThan(lap => lap.GiaVon).WithMessage("gia ban phai lon hon gia nhap");
        }
    }
}
