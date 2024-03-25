using device.Data;
using device.Models;
using device.Response;
using Microsoft.EntityFrameworkCore;

namespace device.Validator
{
    public class PcValidate
    {
        private readonly LaptopDbContext _context;

        public PcValidate(LaptopDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<PrivateComputerModel>> RegexPc(PrivateComputerModel model)
        {
            var producer = await _context.producers.FirstOrDefaultAsync(p => p.Id == model.ProducerId & p.IsDelete == false);

            if (producer == null)
            {
                return new BaseResponse<PrivateComputerModel>
                {
                    Success = false,
                    Message = "Nhà sản xuất không tồn tại",
                    ErrorCode = ErrorCode.NotFound
                };
            }
            return new BaseResponse<PrivateComputerModel>
            {
                Success = true,
                Message = ""
            };
        }
    }
}
