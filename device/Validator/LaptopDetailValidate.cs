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

        public LaptopDetailValidate(LaptopDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<LaptopDetail>> RegexLaptopDetail(LaptopDetailModel model)
        {
            var laptop = await _context.laptops.FindAsync(model.LaptopId);

            var ram = await _context.ram.FindAsync(model.RamId);

            var vga = await _context.vgas.FindAsync(model.VgaId);

            var monitor = await _context.monitors.FindAsync(model.MonitorId);

            var seri = await _context.laptopsDetail.FirstOrDefaultAsync( i => i.Seri == model.Seri);
            
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

            if (model.Cpu.Length >= 50)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Vượt quá kí tự cho phép!!!"
                };
            }

            if (model.HardDriver.Length >= 50)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = "Vượt quá kí tự cho phép!!!"
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
