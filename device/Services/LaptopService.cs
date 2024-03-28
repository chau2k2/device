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
            _validate = new LaptopValidate(context);
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
                        IsDelete = laptop.IsDelete
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
                    Error = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<IEnumerable<LaptopResponse>>>> SearchLaptop(string? name, string? producerName, decimal? firstPrice, decimal? endPrice)
        {
            try
            {
                var laptop = await _context.Set<Laptop>()
                     .Include(l => l.Producer)
                     .Include(l => l.LaptopDetail)
                     .ToListAsync();

                var laptopQuery = (from s in laptop
                                  where (!firstPrice.HasValue || s.CostPrice >= firstPrice) &&
                                        (!endPrice.HasValue || s.CostPrice <= endPrice) &&
                                        (string.IsNullOrEmpty(name) || s.Name.Contains(name)) &&
                                        (string.IsNullOrEmpty(producerName) || s.Producer!.Name == producerName) &&
                                        !s.IsDelete
                                        select s).ToList();

                List<LaptopResponse> laptopResponses = new List<LaptopResponse>();

                foreach ( var lap in laptopQuery )
                {
                    laptopResponses.Add(new LaptopResponse()
                    {
                        Id = lap.Id,
                        ProducerId = lap.ProducerId,
                        Name = lap.Name,
                        CostPrice = lap.CostPrice,
                        SoldPrice = lap.SoldPrice,
                        IsDelete = lap.IsDelete,
                        ProducerName = lap.Producer!.Name
                    }) ;
                }

                return new BaseResponse<IEnumerable<LaptopResponse>> 
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = laptopResponses
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<LaptopResponse>>()
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }

        public async Task<ActionResult<BaseResponse<LaptopResponse>>> GetLaptopById(int id)
        {
            try
            {
                var lap = await _context.Set<Laptop>()
                    .Include(l => l.Producer)
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

        public async Task<ActionResult<BaseResponse<Laptop>>> Updatelaptop(int id, LaptopModel model)
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
                    Name = model.Name,
                    ProducerId = model.ProducerId,
                    CostPrice = model.CostPrice,
                    SoldPrice = model.SoldPrice,
                    IsDelete = model.IsDelete
                };

                var validator = await _validate.RegexLaptop(model);

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
        public async Task<ActionResult<BaseResponse<Laptop>>> CreateLaptop( LaptopModel model)
        {
            try
            {
                int maxId = await _context.laptops.MaxAsync(p => (int?)p.Id) ?? 0;

                int next = maxId + 1;

                Laptop laptop = new Laptop()
                {
                    Id = next,
                    Name = model.Name,
                    ProducerId = model.ProducerId,
                    CostPrice = model.CostPrice,
                    SoldPrice = model.SoldPrice
                };

                var validator = await _validate.RegexLaptop(model);

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
