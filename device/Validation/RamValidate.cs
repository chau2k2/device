using device.Cons;
using device.Models;
using FluentValidation;
using System.Reflection.Metadata;

namespace device.Validation
{
    public class RamValidate: AbstractValidator<Ram>
    {
        public RamValidate()
        {
            RuleFor(ram => ram.Name).NotNull().WithMessage("Name is not null").Must(nam => beInteger(nam) && beInRange(nam))
                .WithMessage($"Laptop must be integer and between {Constants.MIN_RAM} and {Constants.MAX_RAM}");
            RuleFor(ram => ram.Price).InclusiveBetween(0,Constants.MAX_PRICE).WithMessage($"price must be between 0 and {Constants.MAX_PRICE}");
        }
        private bool beInteger (string name)
        {
            return int.TryParse(name, out _);
        }
        private bool beInRange(string name)
        {
            if (int.TryParse(name, out int value))
            {
                return value >= Constants.MIN_RAM && value <= Constants.MAX_RAM;
            }
            return false;
        }
    }
}
