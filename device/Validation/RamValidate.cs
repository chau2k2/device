using device.Models;
using FluentValidation;

namespace device.Validation
{
    public class RamValidate: AbstractValidator<Ram>
    {
        public RamValidate() 
        {
            RuleFor(ram => ram.Name).NotNull().WithMessage("Name is not null");
            RuleFor(ram => ram.Name).Must(beInteger).WithMessage("Name must be an integer");
            RuleFor(ram => ram.Price).GreaterThan(0).WithMessage("price is greater than ");
        }
        private bool beInteger (string name)
        {
            return int.TryParse(name, out _);
        } 
    }
}
