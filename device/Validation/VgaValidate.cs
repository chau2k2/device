using device.Models;
using FluentValidation;
using System.Security.Permissions;

namespace device.Validation
{
    public class VgaValidate: AbstractValidator<Vga>
    {
        public VgaValidate() 
        {
            RuleFor(vga => vga.Name).NotEmpty().WithMessage("name is not empty and not null");
            RuleFor(vga => vga.Name).MaximumLength(Constants.MAX_LENGTH_NAME).WithMessage($"length's name is not greate than {Constants.MAX_LENGTH_NAME}");
            RuleFor(vga => vga.Price).GreaterThan(0).WithMessage("price is greater than 0");
        }
    }
}
