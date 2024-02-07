using device.Cons;
using device.Models;
using FluentValidation;

namespace device.Validation
{
    public class ProducerValidate: AbstractValidator<Producer>
    {
        public ProducerValidate() 
        {
            RuleFor(producer => producer.Name).NotEmpty().WithMessage("Name is not null");
            RuleFor(producer => producer.Name).MaximumLength(Constants.MAX_LENGTH_NAME).WithMessage($"Name is not greater than {Constants.MAX_LENGTH_NAME}");
        }
    }
}
