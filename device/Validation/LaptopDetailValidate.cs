using device.Cons;
using device.Data;
using device.Models;
using FluentValidation;
using Npgsql;
using System.Configuration;

namespace device.Validation
{
    public class LaptopDetailValidate: AbstractValidator<LaptopDetail>
    {
        private readonly LaptopDbContext _dbconext;

        public LaptopDetailValidate(LaptopDbContext dbContext)
        {
            _dbconext = dbContext;
            RuleFor(detail => detail.idLaptop).NotEmpty().WithMessage("Laptop id is not null and not empty");
            RuleFor(detail => detail.Cpu).MaximumLength(Constants.MAX_LENGTH_PUBLIC).WithMessage($"length is not greater than {Constants.MAX_LENGTH_PUBLIC}");
            RuleFor(detail => detail.IdRam).Must(idram => IsRamValue(idram)).WithMessage("IdRam is not exist in table Laptop");
            RuleFor(detail => detail.IdMonitor).Must(idMon => IsMonitor(idMon)).WithMessage("IdMonitor is not exist in table Monitor");
            RuleFor(detail => detail.IdVga).Must(idVga => IsVgaValue(idVga)).WithMessage("IdVga is not exist in table Vga");
            RuleFor(detail => detail.idLaptop).Must(idLap => IsLaptopValue(idLap)).WithMessage("IdLaptop is not exist in table laptop");
            RuleFor(detail => detail.Weight).InclusiveBetween(0,Constants.MAX_WEIGHT).WithMessage($"The weight must be within the range of 0 to {Constants.MAX_WEIGHT}.");
            RuleFor(detail => detail.Height).InclusiveBetween(0, Constants.MAX_LENGTH_PUBLIC).WithMessage($"The height must be within the range of 0 to {Constants.MAX_LENGTH_PUBLIC}.");
            RuleFor(detail => detail.Width).InclusiveBetween(0, Constants.MAX_LENGTH_PUBLIC).WithMessage($"The width must be within the range of 0 to {Constants.MAX_LENGTH_PUBLIC}.");
            RuleFor(detail => detail.Length).InclusiveBetween(0, Constants.MAX_LENGTH_PUBLIC).WithMessage($"The weight must be within the range of 0 to {Constants.MAX_LENGTH_PUBLIC}.");
            RuleFor(detail => detail.BatteryCapacity).InclusiveBetween(0, Constants.MAX_PIN).WithMessage($"The weight must be within the range of 0 to {Constants.MAX_PIN}.");
            RuleFor(detail => detail.Webcam).MaximumLength(Constants.MAX_LENGTH_PUBLIC).WithMessage($"Length of Webcam not greater than {Constants.MAX_LENGTH_PUBLIC}");
            RuleFor(detail => detail.Seri).MaximumLength(Constants.MAX_LENGTH_PUBLIC).WithMessage($"length of seri not greater than {Constants.MAX_LENGTH_PUBLIC}");
            RuleFor(detail => detail.HardDriver).MaximumLength(Constants.MAX_LENGTH_HARD_DRIVER).WithMessage($"length of hard driver not greater than {Constants.MAX_LENGTH_HARD_DRIVER}");
        }
        
        private bool IsRamValue (int idRam)
        {
            return _dbconext.ram.Any(r => r.Id == idRam);
        }
        private bool IsMonitor (int idMonitor)
        {
            return _dbconext.monitors.Any(r => r.Id == idMonitor);
        }
        private bool IsVgaValue ( int idVga)
        {
            return _dbconext.vgas.Any(v => v.Id == idVga);
        }
        private bool IsLaptopValue (int  idLaptop)
        {
            return _dbconext.laptops.Any(l => l.Id == idLaptop);
        }
    }
}
