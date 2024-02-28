using device.Data;
using device.DTO.Ram;
using device.IRepository;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Services
{
    public class RamService
    {
        private readonly ILogger<RamService> _logger;
        private readonly IAllRepository<Ram> _repo;
        private readonly LaptopDbContext _context;
        private readonly RamValidate _validate;

        public RamService( IAllRepository<Ram> repo, ILogger<RamService> logger, LaptopDbContext context) 
        {
            this._logger = logger;
            _repo = repo;
            _context = context;
            _validate = new RamValidate();
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
        public async Task<ActionResult<Ram>> Create (CreateRam CrR)
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
                var validate = _validate.Validate(ram);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }
                var result = await _repo.AddOneAsync(ram);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Ram>> Update (int id, UpdateRam UpR)
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
                var validate = _validate.Validate(ram);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(",", validate.Errors));
                }

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
            var findId = await _repo.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("not found Producer");
            }

            try
            {
                var del = await _repo.DeleteOneAsync(findId);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("cant delete this producer");
            }
        }
    }
}
