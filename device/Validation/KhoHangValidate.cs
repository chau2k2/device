using device.Models;
using FluentValidation;

namespace device.Validation
{
    public class KhoHangValidate: AbstractValidator<Storage>
    {
        public KhoHangValidate()
        {
            RuleFor(khohang => khohang.SaleNumber).NotEmpty().WithMessage("So luong ban khong duoc null");
            RuleFor(khohang => khohang.InserNumber).NotEmpty().WithMessage("so luong nhap khong duoc null");
            RuleFor(khohang => khohang.InserNumber).GreaterThan(0).WithMessage("so luong nhap phai lon hon 0");
            RuleFor(khohang => khohang.SaleNumber).GreaterThan(0).WithMessage("so luong ban phai lon hon 0");
        }
    }
}
