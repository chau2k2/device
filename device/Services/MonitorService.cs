using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;

namespace device.Services
{
    public class MonitorService : IMonitorService
    {
        private readonly ILogger<MonitorService> _logger;
        private readonly IAllRepository<MonitorM> _repos;
        private readonly LaptopDbContext _context;

        public MonitorService(IAllRepository<MonitorM> repos, ILogger<MonitorService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repos = repos;
            _context = context;
        }

        public async Task<IEnumerable<MonitorM>> GetAll(int page, int pageSize)
        {
            try
            {
                var result = await _repos.GetAllAsync(page, pageSize);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<MonitorM>> GetById(int id)
        {
            var result = await _repos.GetAsyncById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }
        public async Task<ActionResult<MonitorM>> Update(int id,MonitorResponse UpM)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("Not found Monitor");
            }
            MonitorM monitor = new MonitorM()
            {
                Id = id,
                Name = UpM.Name,
                Price = UpM.Price
            };
            try
            {
                var result = await _repos.UpdateOneAsyns(monitor);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<MonitorM>> Create(MonitorResponse CrM)
        {
            int maxId = await _context.monitors.MaxAsync(p => (int?)p.Id) ?? 0;
            int next = maxId + 1;

            MonitorM monitor = new MonitorM()
            {
                Id = next,
                Name = CrM.Name,
                Price = CrM.Price
            };

            try
            {
                var result = await _repos.AddOneAsync(monitor);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<MonitorM>> Delete(int id)
        {
            try
            {
                var monitor = await _repos.GetAsyncById(id);
                if (monitor == null)
                {
                    throw new Exception("Not found Monitor");
                }
                monitor.IsDelete = true;
                var del = await _repos.DeleteOneAsync(monitor);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("Can't delete this monitor");
            }
        }
    }
}
