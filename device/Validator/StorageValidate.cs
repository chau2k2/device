using device.Data;
using device.Entity;
using device.Models;
using device.Response;
using Microsoft.EntityFrameworkCore;

namespace device.Validator
{
    public class StorageValidate
    {
        private readonly LaptopDbContext _context;

        public StorageValidate(LaptopDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<StorageModel>> RegexStorage (StorageModel model)
        {
            switch (model.ProductType)
            {
                case EProductType.Laptop:
                    var laptop = await _context.laptops.FirstOrDefaultAsync(s => s.Id == model.ProductId);
                    if (laptop == null)
                    {
                        return new BaseResponse<StorageModel>
                        {
                            Success = false,
                            Message = "NotFound!!!",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;
                case EProductType.PrivateComputer:
                    var pc = await _context.PrivateComputer.FirstOrDefaultAsync(p => p.Id == model.ProductId);
                    if (pc == null)
                    {
                        return new BaseResponse<StorageModel>
                        {
                            Success = false,
                            Message = "NotFound!!!",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;
                case EProductType.Ram:
                    var ram = await _context.ram.FirstOrDefaultAsync(r => r.Id == model.ProductId);
                    if (ram == null)
                    {
                        return new BaseResponse<StorageModel>
                        {
                            Success = false,
                            Message = "NotFound!!!",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;
                case EProductType.Monitor:
                    var monitor = await _context.monitors.FirstOrDefaultAsync(m => m.Id == model.ProductId);
                    if (monitor == null)
                    {
                        return new BaseResponse<StorageModel>
                        {
                            Success = false,
                            Message = "NotFound!!!",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;

                case EProductType.Vga:
                    var vga = await _context.vgas.FirstOrDefaultAsync(v => v.Id == model.ProductId);
                    if (vga == null)
                    {
                        return new BaseResponse<StorageModel>
                        {
                            Success = false,
                            Message = "NotFound!!!",
                            ErrorCode = ErrorCode.NotFound
                        };
                    }
                    break;
            }

            if ((model.ImportNumber - model.SoldNumber) < 0)
            {
                return new BaseResponse<StorageModel>
                {
                    Success = false,
                    Message = "Số lượng bán đang vượt quá số lượng nhập!!!",
                    ErrorCode = ErrorCode.Error
                };
            }

            var storage = await _context.storages.FirstOrDefaultAsync(s => s.ProductType == model.ProductType && s.ProductId == model.ProductId && s.IsDelete == false);

            if (storage != null)
            {
                return new BaseResponse<StorageModel>
                {
                    Success = false,
                    Message = "Đã có kho hàng của sản phẩm này",
                    ErrorCode = ErrorCode.Error
                };
            }
            return new BaseResponse<StorageModel>
            {
                Success = true,
                Message = ""
            };
        }
    }
}
