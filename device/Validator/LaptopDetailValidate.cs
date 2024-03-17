using device.Cons;
using device.Data;
using device.Entity;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

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
        public async Task<ActionResult<BaseResponse<LaptopDetailResponse>>> RegexLaptopDetail(LaptopDetailModel laptopDetail)
        {
            var laptop = await _context.laptops.Include(i => i.Storage).FirstOrDefaultAsync(i => i.Id == laptopDetail.LaptopId);

            var ram = await _context.ram.FindAsync(laptopDetail.RamId);

            var vga = await _context.vgas.FindAsync(laptopDetail.VgaId);

            var monitor = await _context.monitors.FindAsync(laptopDetail.MonitorId);

            if (monitor == null || monitor.IsDelete == true)
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Message = "Monitor khong ton tai hoac da bi xoa!!!"
                };
            }

            if (vga == null || vga.IsDelete == true)
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Message = "Vga khong ton tai hoac da bi xoa"
                };
            }

            if (ram == null || ram.IsDelete == true)
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Message = "Ram khong ton tai hoac da bi xoa!!!"
                };
            }
             
            if (laptop == null || laptop.IsDelete == true)
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Message = "Laptop khong ton tai!"
                };
            }

            if (laptop.Storage.ImportNumber == laptop.Storage.SoldNumber)
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Message = "Laptop nay da het hang!!!"
                };
            }

            if (laptopDetail.Cpu.Length >= 50)
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Message = "Vuot qua ki tu cho phep!!!"
                };
            }

            if (laptopDetail.HardDriver.Length >= 50)
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Message = "Vuot qua ki tu cho phep !!!"
                };
            }

            if (_duplicate.isValueName(laptopDetail.HardDriver))
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Message = "Khong spam!!!"
                };
            }

            if (_duplicate.isValueName(laptopDetail.Cpu))
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Message = "Khong spam!!!"
                };
            }
          
            return new BaseResponse<LaptopDetailResponse>
            {
                Success = true,
                Message = ""
            };
        }
    }
}
