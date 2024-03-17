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

        public LaptopDetailValidate( LaptopDbContext context, CheckDuplicate duplicate) 
        {
            _context = context;
            _duplicate = duplicate;
        }
        public async Task<ActionResult<BaseResponse<LaptopDetailResponse>>> RegexLaptopDetail(LaptopDetailModel laptopDetail)
        {
            var laptop = await _context.laptops.Include(i => i.Storage).FirstOrDefaultAsync(i => i.Id == laptopDetail.LaptopId);

            if (laptopDetail.RamId == )
             
            if (laptop == null || laptop.IsDelete == true)
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Error = "Laptop khong ton tai!"
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
                    Error = "Vuot qua ki tu cho phep!!!"
                };
            }

            if (laptopDetail.HardDriver.Length >= 50)
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Error = "Vuot qua ki tu cho phep !!!"
                };
            }

            if (_duplicate.isValueName(laptopDetail.HardDriver))
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Error = "Khong spam!!!"
                };
            }

            if (_duplicate.isValueName(laptopDetail.Cpu))
            {
                return new BaseResponse<LaptopDetailResponse>
                {
                    Success = false,
                    Error = "Khong spam!!!"
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
