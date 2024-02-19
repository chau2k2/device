using device.Cons;
using device.Models;
using device.Validation.CheckName;
using FluentValidation;

namespace device.Validation
{
    public class ProducerValidate: AbstractValidator<Producer>
    {
        private readonly AllNameRepeat _nameRepeat;
        public ProducerValidate() 
        {
            _nameRepeat = new AllNameRepeat();
            RuleFor(producer => producer.Name).NotEmpty().WithMessage("Name is not null");
            RuleFor(producer => producer.Name).Must(name => _nameRepeat.IsValueName(name)).WithMessage("dont spam name");
            RuleFor(producer => producer.Name).MaximumLength(Constants.MAX_LENGTH_NAME).WithMessage($"Name is not greater than {Constants.MAX_LENGTH_NAME}");
        }  
    }
}
