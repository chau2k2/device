using device.Cons;
using device.Models;
using FluentValidation;

namespace device.Validation
{
    public class MonitorValidate :AbstractValidator<MonitorM>
    {
        public MonitorValidate() 
        { 
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is not null and not empty");
            RuleFor(m => m.Name).MaximumLength(Constants.MAX_LENGTH_NAME).WithMessage($"Name is not greater than {Constants.MAX_LENGTH_NAME}");
            RuleFor(m => m.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}
