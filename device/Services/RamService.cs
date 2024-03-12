using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;

namespace device.Services
{
    public class RamService : IRamService
    {
        private readonly ILogger<RamService> _logger;
        private readonly IAllRepository<Ram> _repo;
        private readonly LaptopDbContext _context;

        public RamService( IAllRepository<Ram> repo, ILogger<RamService> logger, LaptopDbContext context) 
        {
            this._logger = logger;
            _repo = repo;
            _context = context;
        }
        public async Task<IEnumerable<Ram>> GetAll(int page, int pageSize)
        {
            try
            {
                var result = await _repo.GetAllAsync(page, pageSize);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Ram>> GetById (int id)
        {
            var result = await _repo.GetAsyncById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }
        public async Task<ActionResult<Ram>> Create (RamResponse CrR)
        {
            int maxId = await _context.ram.MaxAsync(r => (int?)r.Id) ?? 0;
            int nextId = maxId + 1;

            Ram ram = new Ram()
            {
                Id = nextId,
                Name = CrR.Name,
                Price = CrR.Price
            };

            try
            {
                var result = await _repo.AddOneAsync(ram);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Ram>> Update (int id, RamResponse UpR)
        {
            var findId = await _context.ram.FindAsync(id);
            if (findId == null)
            {
                return new NotFoundResult();
            }

            Ram ram = new Ram()
            {
                Id = id,
                Name = UpR.Name,
                Price= UpR.Price
            };

            try
            {
                var result = await _repo.UpdateOneAsyns(ram);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Ram>> delete (int id)
        {
            try
            {
                var ram = await _repo.GetAsyncById(id);
                if (ram == null)
                {
                    throw new Exception("Not found Producer");
                }
                ram.IsDelete = true;
                var del = await _repo.DeleteOneAsync(ram);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("Can't delete this producer");
            }
        }
    }
}
