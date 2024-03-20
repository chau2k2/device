using device.Data;
using device.IRepository;
using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.IServices;
using device.Models;
using device.Validator;

namespace device.Services
{
    public class LaptopService :ILaptopService
    {
        private readonly IAllRepository<Laptop> _repos;
        private readonly LaptopDbContext _context;
        private readonly LaptopValidate _validate;

        public LaptopService(IAllRepository<Laptop> repos, LaptopDbContext context)
        {
            _repos = repos;
            _context = context;
            _validate = new LaptopValidate(context, new CheckDuplicate());
        }
        public async Task<TPaging<LaptopResponse>> GetAllLaptop(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<Laptop>().CountAsync(l => l.IsDelete == false);

                var result = await _context.Set<Laptop>()!
                    .Include(s => s.Producer)
                    .Where(c => c.IsDelete == false)
                    .Take(pageSize).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<LaptopResponse> laptopResponse = new List<LaptopResponse>();

                foreach (var laptop in result)
                {
                    laptopResponse.Add(new LaptopResponse()
                    {
                        Id = laptop.Id,
                        Name = laptop.Name,
                        ProducerName = laptop.Producer?.Name,
                        CostPrice = laptop.CostPrice,
                        SoldPrice = laptop.SoldPrice,
                        ProducerId = laptop.ProducerId,
                        IsDelete = laptop.IsDelete,
                        inventory = laptop.inventory
                    }) ;
                }

                return new TPaging<LaptopResponse>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = laptopResponse
                };
            }
            catch (Exception ex)
            {
                return new TPaging<LaptopResponse>
                {
                    Message = ex.Message,
                    Error = Error.Error
                };
            }
        }

        public async Task<ActionResult<BaseResponse<LaptopResponse>>> GetLaptopById(int id)
        {
            try
            {
                var lap = await _context.Set<Laptop>()
                    .Include(l => l.Producer)
                    .Include(l => l.Storage)
                    .Where(l => l.IsDelete == false)
                    .FirstOrDefaultAsync(l => l.Id == id);

                if (lap == null)
                {
                    return new BaseResponse<LaptopResponse>
                    {
                        Success = false,
                        Message = "Not Found!!!",
                        ErrorCode = ErrorCode.NotFound
                    };
                }

                LaptopResponse laptopResponse = new LaptopResponse()
                {
                    Id = lap.Id,
                    Name = lap.Name,
                    ProducerName = lap.Producer?.Name,
                    CostPrice = lap.CostPrice,
                    SoldPrice = lap.SoldPrice,
                    ProducerId = lap.ProducerId,
                    inventory = lap.inventory,
                    IsDelete = lap.IsDelete
                };
               
                return new BaseResponse<LaptopResponse>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = laptopResponse
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<LaptopResponse>()
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }

        public async Task<ActionResult<BaseResponse<IEnumerable< LaptopResponse>>>> FindLaptopByName(string name)
        {
            try
            {
                var laptop = await _context.Set<Laptop>()
                     .Include(l => l.Producer)
                     .Include(l => l.LaptopDetail)
                     .Where( l => l.Name.Contains(name))
                     .ToListAsync();

                List<LaptopResponse> laptopResponse = new List<LaptopResponse>();

                foreach (var lap in laptop)
                {
                    laptopResponse.Add(new LaptopResponse()
                    {
                        Id = lap.Id,
                        Name = lap.Name,
                        ProducerName = lap.Producer?.Name,
                        CostPrice = lap.CostPrice,
                        SoldPrice = lap.SoldPrice,
                        ProducerId = lap.ProducerId,
                        IsDelete = lap.IsDelete,
                        inventory = lap.inventory
                    });
                }

                if (laptop.Any())
                {
                    return new BaseResponse<IEnumerable<LaptopResponse>>
                    {
                        Success = true,
                        Message = "Successfull",
                        ErrorCode = ErrorCode.None,
                        Data = laptopResponse
                    };
                }
                else
                {
                    return new BaseResponse<IEnumerable<LaptopResponse>>
                    {
                        Success = false,
                        Message = "NotFound!!!",
                        ErrorCode = ErrorCode.NotFound
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<LaptopResponse>>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Laptop>>> Updatelaptop(int id, LaptopModel laptopModel)
        {
            try
            {
                var lap = await _repos.GetAsyncById(id);

                if (lap == null || lap!.IsDelete == true)
                {
                    return new BaseResponse<Laptop>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }

                Laptop laptop = new Laptop()
                {
                    Id = id,
                    Name = laptopModel.Name,
                    ProducerId = laptopModel.ProducerId,
                    CostPrice = laptopModel.CostPrice,
                    SoldPrice = laptopModel.SoldPrice,
                    IsDelete = laptopModel.IsDelete
                };

                var validator = await _validate.RegexLaptop(laptopModel);

                if (!validator.Success)
                {
                    return new BaseResponse<Laptop>
                    {
                        Message = validator.Message,
                        ErrorCode = validator.ErrorCode
                    };
                }

                var result = await _repos.UpdateOneAsyns(laptop);

                return new BaseResponse<Laptop>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Laptop>()
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Laptop>>> CreateLaptop( LaptopModel laptopModel)
        {
            try
            {
                int maxId = await _context.laptops.MaxAsync(p => (int?)p.Id) ?? 0;

                int next = maxId + 1;

                Laptop laptop = new Laptop()
                {
                    Id = next,
                    Name = laptopModel.Name,
                    ProducerId = laptopModel.ProducerId,
                    CostPrice = laptopModel.CostPrice,
                    SoldPrice = laptopModel.SoldPrice
                };

                var validator = await _validate.RegexLaptop(laptopModel);

                if (!validator.Success)
                {
                    return new BaseResponse<Laptop>
                    {
                        Message = validator.Message,
                        ErrorCode = validator.ErrorCode
                    };
                }

                var result = await _repos.AddOneAsync(laptop);

                return new BaseResponse<Laptop> 
                { 
                    Success = true, 
                    Message = "Successfull!!!", 
                    Data = result 
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Laptop>()
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Laptop>>> DeleteLaptop(int id)
        {
            try
            {
                var laptop = await _repos.GetAsyncById(id);

                if (laptop == null || laptop!.IsDelete == true)
                {
                    return new BaseResponse<Laptop>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }

                laptop.IsDelete = true;

                var del = await _repos.UpdateOneAsyns(laptop);

                return new BaseResponse<Laptop>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = del
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Laptop>()
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
    }
}
