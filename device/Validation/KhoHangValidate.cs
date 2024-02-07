using device.Models;
using FluentValidation;

namespace device.Validation
{
    public class KhoHangValidate: AbstractValidator<KhoHang>
    {
        public KhoHangValidate()
        {
            RuleFor(khohang => khohang.SoLuongBan).NotEmpty().WithMessage("So luong ban khong duoc null");
            RuleFor(khohang => khohang.SoLuongNhap).NotEmpty().WithMessage("so luong nhap khong duoc null");
            RuleFor(khohang => khohang.SoLuongNhap).GreaterThan(0).WithMessage("so luong nhap phai lon hon 0");
            RuleFor(khohang => khohang.SoLuongBan).GreaterThan(0).WithMessage("so luong ban phai lon hon 0");
        }
    }
}
