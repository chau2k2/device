using device.Data;
using device.Entity;
using device.Models;
using device.Response;
using Microsoft.EntityFrameworkCore;

namespace device.Validator
{
    public class LaptopDetailValidate
    {
        private readonly LaptopDbContext _context;
        private readonly CheckDuplicate _duplicate;

        public LaptopDetailValidate(LaptopDbContext context, CheckDuplicate duplicate)
        {
            _context = context;
            _duplicate = duplicate;
        }
        public async Task<BaseResponse<LaptopDetail>> RegexLaptopDetail(LaptopDetailModel laptopDetail)
        {
            var laptop = await _context.laptops.Include(i => i.Storage).FirstOrDefaultAsync(i => i.Id == laptopDetail.LaptopId);

            var ram = await _context.ram.FindAsync(laptopDetail.RamId);

            var vga = await _context.vgas.FindAsync(laptopDetail.VgaId);

            var monitor = await _context.monitors.FindAsync(laptopDetail.MonitorId);

            var seri = await _context.laptopsDetail.FirstOrDefaultAsync( i => i.Seri == laptopDetail.Seri);
            
            if (seri != null)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Seri không trùng lặp!!!"
                };
            }

            if (monitor == null || monitor.IsDelete == true)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Monitor không tồn tại hoặc đã bị xóa!!!"
                };
            }

            if (vga == null || vga.IsDelete == true)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Vga không tồn tại hoặc đã bị xóa!!!"
                };
            }

            if (ram == null || ram.IsDelete == true)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Ram không tồn tại hoặc đã bị xóa!!!"
                };
            }
             
            if (laptop == null || laptop.IsDelete == true)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Laptop không tồn tại hoặc đã bị xóa!"
                };
            }

            if (laptop.Storage.ImportNumber == laptop.Storage.SoldNumber)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Laptop này đã hết hàng!!!"
                };
            }

            if (laptopDetail.Cpu.Length >= 50)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Vượt quá kí tự cho phép!!!"
                };
            }

            if (laptopDetail.HardDriver.Length >= 50)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Vượt quá kí tự cho phép!!!"
                };
            }

            if (_duplicate.isValueName(laptopDetail.HardDriver))
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Không spam!!!"
                };
            }

            if (_duplicate.isValueName(laptopDetail.Cpu))
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Không spam!!!"
                };
            }
          
            return new BaseResponse<LaptopDetail>
            {
                Success = true,
                Message = ""
            };
        }
    }
}
