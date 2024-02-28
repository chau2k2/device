using device.Data;
using device.DTO.Storage;
using device.DTO.Vga;
using device.IRepository;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Services
{
    public class StorageService
    {
        private readonly ILogger<StorageService> _logger;
        private readonly IAllRepository<Storage> _repo;
        private readonly LaptopDbContext _context;
        private readonly StorageValidate _validate;

        public StorageService(IAllRepository<Storage> repo, ILogger<StorageService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repo = repo;
            _context = context;
            _validate = new StorageValidate();
        }
        public async Task<IEnumerable<Storage>> GetAll(int page, int pageSize)
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
        public async Task<ActionResult<Storage>> GetById(int id)
        {
            var result = await _repo.GetAsyncById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }
        public async Task<ActionResult<Storage>> Create(CreateStorage CrS)
        {
            int maxId = await _context.storages.MaxAsync(r => (int?)r.Id) ?? 0;
            int nextId = maxId + 1;

            Storage storage = new Storage()
            {
                Id = nextId,
                idDetail = CrS.idDetail,
                ImportNumber = CrS.InserNumber,
                SoldNumber = CrS.SaleNumber
            };

            try
            {
                var validate = _validate.Validate(storage);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }
                var result = await _repo.AddOneAsync(storage);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Storage>> Update(int id, UpdateStorage UpS)
        {
            var findId = await _context.ram.FindAsync(id);
            if (findId == null)
            {
                return new NotFoundResult();
            }

            Storage storage = new Storage()
            {
                Id = id,
                idDetail = UpS.idDetail,
                ImportNumber = UpS.InserNumber,
                SoldNumber = UpS.SaleNumber
            };

            try
            {
                var validate = _validate.Validate(storage);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(",", validate.Errors));
                }

                var result = await _repo.UpdateOneAsyns(storage);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Storage>> delete(int id)
        {
            var findId = await _repo.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("not found Vga");
            }

            try
            {
                var del = await _repo.DeleteOneAsync(findId);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("cant delete this vga");
            }
        }
    }
}
