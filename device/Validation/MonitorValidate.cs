using device.Cons;
using device.Models;
using device.Validation.CheckName;
using FluentValidation;

namespace device.Validation
{
    public class MonitorValidate :AbstractValidator<MonitorM>
    {
        private readonly AllNameRepeat _nameRepeat;
        public MonitorValidate() 
        { 
            _nameRepeat = new AllNameRepeat();
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is not null and not empty");
            RuleFor(m => m.Name).MaximumLength(Constants.MAX_LENGTH_NAME).WithMessage($"Name is not greater than {Constants.MAX_LENGTH_NAME}");
            RuleFor(m => m.Name).Must(name => _nameRepeat.IsValueName(name)).WithMessage($"dont spam name");
            RuleFor(m => m.Price).InclusiveBetween(0, Constants.MAX_PRICE).WithMessage($"price must be between 0 and {Constants.MAX_PRICE}");
        }
    }
}
