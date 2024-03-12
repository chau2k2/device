using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;

namespace device.Services
{
    public class ProducerService : IProducerService
    {
        private readonly ILogger<ProducerService> _logger;
        private readonly IAllRepository<Producer> _repos;
        private readonly LaptopDbContext _context;

        public ProducerService(IAllRepository<Producer> repos, ILogger<ProducerService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repos = repos;
            _context = context;
        }

        public async Task<IEnumerable<Producer>> GetAll(int page = 1, int pageSize = 5)
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
        public async Task<ActionResult<Producer>> GetProducerById( int id)
        {
            var result = await _repos.GetAsyncById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }
        public async Task<ActionResult<Producer>> UpdateProducer (int id, ProducerResponse Upd)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("Not found Producer");
            }
            Producer producer = new Producer()
            {
                Id = id,
                Name = Upd.Name,
                IsActive = Upd.IsActive
            };
            try
            {
                var result = await _repos.UpdateOneAsyns(producer);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Producer>> CreateProducer (ProducerResponse cpr)
        {
            int maxId = await _context.producers.MaxAsync(p => (int?)p.Id) ?? 0;
            int next = maxId + 1;

            Producer producer = new Producer()
            {
                Id = next,
                Name = cpr.Name,
                IsActive = cpr.IsActive
            };

            try
            {
                    var result = await _repos.AddOneAsync(producer);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Producer>> DeleteProducer(int id)
        {
            try
            {
                var producer = await _repos.GetAsyncById(id);
                if (producer == null)
                {
                    throw new Exception("Not found Producer");
                }
                producer.IsDelete = true;
                var del = await _repos.DeleteOneAsync(producer);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("Can't delete this producer");
            }
        }
    }
}
