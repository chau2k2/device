using device.Data;
using device.DTO.Ram;
using device.DTO.Vga;
using device.IRepository;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Services
{
    public class VgaService
    {
        private readonly ILogger<VgaService> _logger;
        private readonly IAllRepository<Vga> _repo;
        private readonly LaptopDbContext _context;
        private readonly VgaValidate _validate;

        public VgaService(IAllRepository<Vga> repo, ILogger<VgaService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repo = repo;
            _context = context;
            _validate = new VgaValidate();
        }
        public async Task<IEnumerable<Vga>> GetAll(int page, int pageSize)
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
        public async Task<ActionResult<Vga>> GetById(int id)
        {
            var result = await _repo.GetAsyncById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }
        public async Task<ActionResult<Vga>> Create(CreateVga CrV)
        {
            int maxId = await _context.vgas.MaxAsync(r => (int?)r.Id) ?? 0;
            int nextId = maxId + 1;

            Vga vga = new Vga()
            {
                Id = nextId,
                Name = CrV.Name,
                Price = CrV.Price
            };

            try
            {
                var validate = _validate.Validate(vga);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }
                var result = await _repo.AddOneAsync(vga);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Vga>> Update(int id, UpdateVga UpV)
        {
            var findId = await _context.ram.FindAsync(id);
            if (findId == null)
            {
                return new NotFoundResult();
            }

            Vga vga = new Vga()
            {
                Id = id,
                Name = UpV.Name,
                Price = UpV.Price
            };

            try
            {
                var validate = _validate.Validate(vga);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(",", validate.Errors));
                }

                var result = await _repo.UpdateOneAsyns(vga);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Vga>> delete(int id)
        {
            try
            {
                var vga = await _repo.GetAsyncById(id);
                if (vga == null)
                {
                    throw new Exception("not found Vga");
                }
                vga.IsDelete = true;
                var del = await _repo.UpdateOneAsyns(vga);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("cant delete this vga");
            }
        }
    }
}
