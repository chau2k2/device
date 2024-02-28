using device.Data;
using device.DTO.Monitor;
using device.IRepository;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Services
{
    public class MonitorService
    {
        private readonly ILogger<MonitorService> _logger;
        private readonly IAllRepository<MonitorM> _repos;
        private readonly MonitorValidate _validate;
        private readonly LaptopDbContext _context;

        public MonitorService(IAllRepository<MonitorM> repos, ILogger<MonitorService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repos = repos;
            _validate = new MonitorValidate();
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
        public async Task<ActionResult<MonitorM>> Update(int id,UpdateMonitor UpM)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("not found Monitor");
            }
            MonitorM monitor = new MonitorM()
            {
                Id = id,
                Name = UpM.Name,
                Price = UpM.Price
            };
            try
            {
                var validate = _validate.Validate(monitor);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }

                var result = await _repos.UpdateOneAsyns(monitor);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<MonitorM>> Create(CreateMonitor CrM)
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
                var validate = _validate.Validate(monitor);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }
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
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("not found Monitor");
            }

            try
            {
                var del = await _repos.DeleteOneAsync(findId);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("cant delete this monitor");
            }
        }
    }
}
