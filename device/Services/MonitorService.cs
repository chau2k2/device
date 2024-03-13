using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;
using device.Response;

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
                int totalCount = _context.Set<MonitorM>().Count();

                var result = await _repos.GetAllAsync(page, pageSize);

                return new TPaging<MonitorM>
                {
                    numberPage = page,
                    totalRecord = totalCount,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
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
                        success = false,
                        message = "NotFound!!!"
                    };
                }
                return new BaseResponse<MonitorM>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = result
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<MonitorM>>> Update(int id,MonitorResponse UpM)
        {
            try
            {
                var findId = await _repos.GetAsyncById(id);
                
                if (findId == null)
                {
                    return new BaseResponse<MonitorM>
                    {
                        success = false,
                        message = "NotFound!!!"
                    };
                }

                MonitorM monitor = new MonitorM()
                {
                    Id = id,
                    Name = UpM.Name,
                    Price = UpM.Price
                };
            
                var result = await _repos.UpdateOneAsyns(monitor);

                return new BaseResponse<MonitorM>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<MonitorM>>> Create(MonitorResponse CrM)
        {
            try
            {
                int maxId = await _context.monitors.MaxAsync(p => (int?)p.Id) ?? 0;
                int next = maxId + 1;

                MonitorM monitor = new MonitorM()
                {
                    Id = next,
                    Name = CrM.Name,
                    Price = CrM.Price
                };

            
                var result = await _repos.AddOneAsync(monitor);

                return new BaseResponse<MonitorM>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
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
                        success = false,
                        message = "NotFound!!!"
                    };
                }

                monitor.IsDelete = true;

                var del = await _repos.DeleteOneAsync(monitor);

                return new BaseResponse<MonitorM>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = del
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
