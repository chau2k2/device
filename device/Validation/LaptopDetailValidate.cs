using device.Cons;
using device.Models;
using FluentValidation;
using Microsoft.AspNetCore.Rewrite;

namespace device.Validation
{
    public class LaptopDetailValidate: AbstractValidator<LaptopDetail>
    {
        public LaptopDetailValidate()
        {
            RuleFor(detail => detail.idLaptop).NotEmpty().WithMessage("Laptop id is not null and not empty");
            RuleFor(detail => detail.Cpu).MaximumLength(Constants.MAX_LENGTH_PUBLIC).WithMessage($"length is not greater than {Constants.MAX_LENGTH_PUBLIC}");
            RuleFor(detail => detail.Weight).GreaterThan(0).WithMessage("weight must be greater than 0");
            RuleFor(detail => detail.Height).GreaterThan(0).WithMessage("height must be greater than 0");
            RuleFor(detail => detail.Width).GreaterThan(0).WithMessage("width must be greate than 0");
            RuleFor(detail => detail.Length).GreaterThan(0).WithMessage("Length must be greater than 0");
            RuleFor(detail => detail.BatteryCatttery).GreaterThan(0).WithMessage("Battery cattery must be greater than 0");
            RuleFor(detail => detail.Webcam).MaximumLength(Constants.MAX_LENGTH_PUBLIC).WithMessage($"Length of Webcam not greater than {Constants.MAX_LENGTH_PUBLIC}");
            RuleFor(detail => detail.Seri).MaximumLength(Constants.MAX_LENGTH_PUBLIC).WithMessage($"length of seri not greater than {Constants.MAX_LENGTH_PUBLIC}");
            RuleFor(detail => detail.HardDriver).MaximumLength(Constants.MAX_LENGTH_HARD_DRIVER).WithMessage($"length of hard driver not greater than {Constants.MAX_LENGTH_HARD_DRIVER}");
        }
    }
}
