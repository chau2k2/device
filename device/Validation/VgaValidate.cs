using device.Cons;
using device.Models;
using device.Validation.CheckName;
using FluentValidation;
using System.Security.Permissions;

namespace device.Validation
{
    public class VgaValidate: AbstractValidator<Vga>
    {
        private readonly AllNameRepeat _nameRepeat;
        public VgaValidate() 
        {
            _nameRepeat = new AllNameRepeat();
            RuleFor(vga => vga.Name).NotEmpty().WithMessage("name is not empty and not null");
            RuleFor(vga => vga.Name).MaximumLength(Constants.MAX_LENGTH_PUBLIC).WithMessage($"length's name is not greate than {Constants.MAX_LENGTH_PUBLIC}");
            RuleFor(vga => vga.Name).Must(name => _nameRepeat.IsValueName(name)).WithMessage("Dont spam name");
            RuleFor(vga => vga.Price).InclusiveBetween(0,Constants.MAX_PRICE).WithMessage($"price must be between 0 and {Constants.MAX_PRICE}");
        }
    }
}
