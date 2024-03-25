using device.Cons;
using device.Data;
using device.Models;
using device.Response;
using Microsoft.EntityFrameworkCore;

namespace device.Validator
{
    public class LaptopValidate
    {
        private readonly LaptopDbContext _context;

        public LaptopValidate(LaptopDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<LaptopModel>> RegexLaptop(LaptopModel laptop)
        {
            var producer = await _context.producers.FirstOrDefaultAsync(p => p.Id == laptop.ProducerId);

            if (laptop.Name.Length >= Constants.MAX_LENGTH_NAME)
            {
                return new BaseResponse<LaptopModel>
                {
                    Success = false,
                    Message = "Vượt quá kí tự cho phép!!!"
                };
            }

            if (producer == null)
            {
                return new BaseResponse<LaptopModel>
                {
                    Success = false,
                    Message = "Nhà sản xuất( Producer) không tồn tại!!!"
                };
            }

            if (laptop.SoldPrice < laptop.CostPrice)
            {
                return new BaseResponse<LaptopModel>
                {
                    Success = false,
                    Message = "Giá bán phải lớn hơn hoặc bằng giá nhập!!!"
                };
            }

            if (laptop.SoldPrice >= Constants.MAX_PRICE || laptop.SoldPrice <= 0)
            {
                return new BaseResponse<LaptopModel>
                {
                    Success = false,
                    Message = $"Giá bán phải là số dương và lớn hơn {Constants.MAX_PRICE}"
                };
            }

            if (laptop.CostPrice >= Constants.MAX_PRICE || laptop.CostPrice <= 0)
            {
                return new BaseResponse<LaptopModel>
                {
                    Success = false,
                    Message = $"Giá nhập phải là số dương và lớn hơn {Constants.MAX_PRICE}"
                };
            }
            //check so luong
            var storage = _context.storages.FirstOrDefault();

            return new BaseResponse<LaptopModel>
            {
                Success = true,
                Message = ""
            };
        }
    }
}
