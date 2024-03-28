using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.IServices;
using device.Response;
using device.Models;

namespace device.Services
{
    public class MonitorService : IMonitorService
    {
        private readonly IAllRepository<MonitorM> _repos;
        private readonly LaptopDbContext _context;

        public MonitorService(IAllRepository<MonitorM> repos, LaptopDbContext context)
        {
            _repos = repos;
            _context = context;
        }

        public async Task<TPaging<MonitorM>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = _context.Set<MonitorM>().Count(i => i.IsDelete == false);

                var result = await _repos.GetAllAsync(page, pageSize);

                return new TPaging<MonitorM>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new TPaging<MonitorM>
                {
                    Message = ex.Message,
                    Error = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<MonitorM>>> GetById(int id)
        {
            try
            {
                var result = await _repos.GetAsyncById(id);
                if (result == null)
                {
                    return new BaseResponse<MonitorM>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }
                return new BaseResponse<MonitorM>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<MonitorM>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<MonitorM>>> Update(int id, MonitorModel model)
        {
            try
            {
                var findId = await _repos.GetAsyncById(id);
                
                if (findId == null)
                {
                    return new BaseResponse<MonitorM>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }

                MonitorM monitor = new MonitorM()
                {
                    Id = id,
                    Name = model.Name,
                    Price = model.Price
                };
            
                var result = await _repos.UpdateOneAsyns(monitor);

                return new BaseResponse<MonitorM>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MonitorM>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<MonitorM>>> Create(MonitorModel model)
        {
            try
            {
                int maxId = await _context.monitors.MaxAsync(p => (int?)p.Id) ?? 0;
                int next = maxId + 1;

                MonitorM monitor = new MonitorM()
                {
                    Id = next,
                    Name = model.Name,
                    Price = model.Price
                };

            
                var result = await _repos.AddOneAsync(monitor);

                return new BaseResponse<MonitorM>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MonitorM>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<MonitorM>>> Delete(int id)
        {
            try
            {
                var monitor = await _repos.GetAsyncById(id);

                if (monitor == null)
                {
                    return new BaseResponse<MonitorM>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }

                monitor.IsDelete = true;

                var del = await _repos.DeleteOneAsync(monitor);

                return new BaseResponse<MonitorM>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = del
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MonitorM>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
    }
}
