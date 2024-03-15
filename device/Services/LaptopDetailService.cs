using device.Data;
using device.IRepository;
using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.IServices;

namespace device.Services
{
    public class LaptopDetailService : ILaptopDetailService
    {
        private readonly IAllRepository<LaptopDetail> _repos;
        private readonly LaptopDbContext _context;

        public LaptopDetailService(IAllRepository<LaptopDetail> repos, LaptopDbContext context)
        {
            _repos = repos;
            _context = context;
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
                    .Take(page).Skip((page - 1) * pageSize)
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
                throw ex;
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
                    Data = result
                };
            }
            catch( Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<ActionResult<BaseResponse<LaptopDetail>>> Update(int id, LaptopDetailResponse UpLD)
        {
            try
            {
                var detail = await _repos.GetAsyncById(id);

                if (detail == null || detail!.IsDelete == true)
                {
                    return new BaseResponse<LaptopDetail>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }

                LaptopDetail laptopDetail = new LaptopDetail()
                {
                    Id = id,
                    Cpu = UpLD.Cpu,
                    Seri = UpLD.Seri,
                    VgaId = UpLD.VgaId,
                    RamId = UpLD.RamId,
                    HardDriver = UpLD.HardDriver,
                    MonitorId = UpLD.MonitorId,
                    Webcam = UpLD.Webcam,
                    Weight = UpLD.Weight,
                    Height = UpLD.Height,
                    Width = UpLD.Width,
                    Length = UpLD.Length,
                    BatteryCapacity = UpLD.BatteryCapacity,
                    LaptopId = UpLD.LaptopId
                };
            
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
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<LaptopDetail>>> Create(LaptopDetailResponse CrLD)
        {
            try
            {
                int maxId = await _context.laptopsDetail.MaxAsync(p => (int?)p.Id) ?? 0;
                int next = maxId + 1;

                LaptopDetail laptopDetail = new LaptopDetail()
                {
                    Id = next,
                    Cpu = CrLD.Cpu,
                    Seri = CrLD.Seri,
                    VgaId = CrLD.VgaId,
                    RamId = CrLD.RamId,
                    HardDriver = CrLD.HardDriver,
                    MonitorId = CrLD.MonitorId,
                    Webcam = CrLD.Webcam,
                    Weight = CrLD.Weight,
                    Height = CrLD.Height,
                    Width = CrLD.Width,
                    Length = CrLD.Length,
                    BatteryCapacity = CrLD.BatteryCapacity,
                    LaptopId = CrLD.LaptopId,
                    IsDelete = CrLD.IsDelete
                };

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
                throw ex;
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

                var del = await _repos.DeleteOneAsync(laptopDetail);

                return new BaseResponse<LaptopDetail>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = del
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
