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
    public class LaptopDetailService : ILaptopDetailService
    {
        private readonly IAllRepository<LaptopDetail> _repos;
        private readonly LaptopDbContext _context;
        private readonly ILogger<LaptopDetailService> _logger;
        private readonly LaptopDetailValidate _validate;

        public LaptopDetailService( IAllRepository<LaptopDetail> repos, LaptopDbContext context, ILogger<LaptopDetailService> logger)
        {
            _repos = repos;
            _context = context;
            _validate = new LaptopDetailValidate(context);
            _logger = logger;
        }

        public async Task<TPaging<LaptopDetailResponse>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<LaptopDetail>().CountAsync(i => i.IsDelete == false);

                int totalPage = (int)Math.Ceiling((double)totalCount / pageSize);

                var result = await _context.Set<LaptopDetail>()!
                    .Include(l => l.Laptops)
                    .Include(r => r.Rams)
                    .Include(m => m.Monitor)
                    .Include(v => v.Vga)
                    .Where(d => d.IsDelete == false)
                    .Take(pageSize).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<LaptopDetailResponse> laptopDetailResponse = new List<LaptopDetailResponse>();

                foreach (var laptopDetail in result)
                {
                    laptopDetailResponse.Add(new LaptopDetailResponse()
                    {
                        Id = laptopDetail.Id,
                        Cpu = laptopDetail.Cpu,
                        Seri = laptopDetail.Seri,
                        VgaName = laptopDetail.Vga.Name,
                        RamName = laptopDetail.Rams.Name,
                        HardDriver = laptopDetail.HardDriver,
                        MonitorName = laptopDetail.Monitor.Name,
                        Webcam = laptopDetail.Webcam,
                        Weight = laptopDetail.Weight,
                        Height = laptopDetail.Height,
                        Width = laptopDetail.Width,
                        Length = laptopDetail.Length,
                        BatteryCapacity = laptopDetail.BatteryCapacity,
                        LaptopName = laptopDetail.Laptops.Name,
                        IsDelete = laptopDetail.IsDelete,
                        LaptopId = laptopDetail.Laptops.Id,
                        MonitorId = laptopDetail.Monitor.Id,
                        RamId = laptopDetail.Rams.Id,
                        VgaId = laptopDetail.Vga.Id
                    }) ;
                }
                return new TPaging<LaptopDetailResponse>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = laptopDetailResponse
                };
            }
            catch (Exception ex)
            {
                return new TPaging<LaptopDetailResponse>
                {
                    Message = ex.Message,
                    Error = Error.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<LaptopDetail>>> GetById(int id)
        {
            try
            {
                var result = await _repos.GetAsyncById(id);

                if (result == null || result!.IsDelete == true)
                {
                    return new BaseResponse<LaptopDetail>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }

                return new BaseResponse<LaptopDetail>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    ErrorCode = ErrorCode.None,
                    Data = result
                };
            }
            catch( Exception ex)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
            
        }
        public async Task<ActionResult<BaseResponse<LaptopDetail>>> Update(int id, LaptopDetailModel model)
        {
            try
            {
                var detail = await _repos.GetAsyncById(id);

                if (detail == null || detail!.IsDelete == true)
                {
                    return new BaseResponse<LaptopDetail>
                    {
                        Success = false,
                        Message = "NotFound!!!",
                        ErrorCode = ErrorCode.NotFound
                    };
                }

                LaptopDetail laptopDetail = new LaptopDetail()
                {
                    Id = id,
                    Cpu = model.Cpu,
                    Seri = model.Seri,
                    VgaId = model.VgaId,
                    RamId = model.RamId,
                    HardDriver = model.HardDriver,
                    MonitorId = model.MonitorId,
                    Webcam = model.Webcam,
                    Weight = model.Weight,
                    Height = model.Height,
                    Width = model.Width,
                    Length = model.Length,
                    BatteryCapacity = model.BatteryCapacity,
                    LaptopId = model.LaptopId
                };

                var validator = await _validate.RegexLaptopDetail(model);

                if (!validator.Success)
                {
                    return new BaseResponse<LaptopDetail>
                    {
                        Message = validator.Message,
                        ErrorCode = validator.ErrorCode
                    };
                }

                var result = await _repos.UpdateOneAsyns(laptopDetail);

                return new BaseResponse<LaptopDetail>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    ErrorCode = ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
        public async Task<ActionResult<BaseResponse<LaptopDetail>>> Create(LaptopDetailModel model)
        {
            try
            {
                int maxId = await _context.laptopsDetail.MaxAsync(p => (int?)p.Id) ?? 0;

                int next = maxId + 1;

                LaptopDetail laptopDetail = new LaptopDetail()
                {
                    Id = next,
                    Cpu = model.Cpu,
                    Seri = model.Seri,
                    VgaId = model.VgaId,
                    RamId = model.RamId,
                    HardDriver = model.HardDriver,
                    MonitorId = model.MonitorId,
                    Webcam = model.Webcam,
                    Weight = model.Weight,
                    Height = model.Height,
                    Width = model.Width,
                    Length = model.Length,
                    BatteryCapacity = model.BatteryCapacity,
                    LaptopId = model.LaptopId,
                    IsDelete = model.IsDelete
                };

                var validator = await _validate.RegexLaptopDetail(model);

                if (!validator.Success)
                {
                    return new BaseResponse<LaptopDetail>
                    {
                        Message = validator.Message
                    };
                }

                var result = await _repos.AddOneAsync(laptopDetail);

                return new BaseResponse<LaptopDetail>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    ErrorCode = ErrorCode.Error,
                    Message = ex.Message
                };
            }
        }
        public async Task<ActionResult<BaseResponse<LaptopDetail>>> Delete(int id)
        {
            try
            {
                var laptopDetail = await _repos.GetAsyncById(id);

                if (laptopDetail == null || laptopDetail!.IsDelete == true)
                {
                    return new BaseResponse<LaptopDetail>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }

                laptopDetail.IsDelete = true;

                var del = await _repos.UpdateOneAsyns(laptopDetail);

                return new BaseResponse<LaptopDetail>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = del
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<LaptopDetail>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
    }
}
