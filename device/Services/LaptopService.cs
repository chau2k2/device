using device.Data;
using device.DTO.Laptop;
using device.IRepository;
using device.Models;
using device.Response;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Services
{
    public class LaptopService
    {
        private readonly ILogger<LaptopService> _logger;
        private readonly IAllRepository<Laptop> _repos;
        private readonly LaptopValidate _validate;
        private readonly LaptopDbContext _context;

        public LaptopService(IAllRepository<Laptop> repos, ILogger<LaptopService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repos = repos;
            _validate = new LaptopValidate();
            _context = context;
        }
        public async Task<IEnumerable<LaptopResponse>> GetAllLaptop(int page = 1, int pageSize = 5)
        {
            try
            {
                var result = await _context.Set<Laptop>()
                    .Include(s=>s.producer)
                    .Include(d => d.laptopDetail)
                        .ThenInclude(r => r.Rams)
                    .Include( d=> d.laptopDetail)
                        .ThenInclude(v => v.Vga)
                    .Include(d => d.laptopDetail)
                        .ThenInclude(s => s.storage)
                    .Include(d => d.laptopDetail)
                        .ThenInclude(m => m.Monitor)
                    .Where(c=>c.SoldPrice > 1)
                    .Take(page).Skip((page - 1) * pageSize)
                    .ToListAsync();

                var  laptopResponse = new List< LaptopResponse>();

                foreach(var laptop in result)
                {
                    laptopResponse.Add(new LaptopResponse()
                    {
                        Id = laptop.Id,
                        Name = laptop.Name,
                        Profit = laptop.SoldPrice - laptop.CostPrice,
                        ProducerName = laptop.producer!.Name,
                        RamName = laptop.laptopDetail.Rams.Name,
                        VgaName = laptop.laptopDetail.Vga.Name,
                        Quantity = laptop.laptopDetail.storage.ImportNumber - laptop.laptopDetail.storage.SoldNumber,
                        MonitorName = laptop.laptopDetail.Monitor.Name
                    });
                }
  

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Laptop>> GetLaptopById(int id)
        {
            var result = await _repos.GetAsyncById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }
        public async Task<ActionResult<Laptop>> Updatelaptop(int id, UpdateLaptop Upd)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("not found Producer");
            }

            Laptop laptop = new Laptop()
            {
                Id = id,
                Name = Upd.Name,
                IdProducer = Upd.IdProducer,
                CostPrice = Upd.CostPrice,
                SoldPrice = Upd.SoldPrice
            };
            try
            {
                var validate = _validate.Validate(laptop);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }
                var result = await _repos.UpdateOneAsyns(laptop);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Laptop>> CreateLaptop(CreateLaptop crl)
        {
            int maxId = await _context.producers.MaxAsync(p => (int?)p.Id) ?? 0;
            int next = maxId + 1;

            Laptop laptop = new Laptop()
            {
                Id = next,
                Name = crl.Name,
                IdProducer = crl.IdProducer,
                CostPrice = crl.CostPrice,
                SoldPrice = crl.SoldPrice
            };

            try
            {
                var validate = _validate.Validate(laptop);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }
                var result = await _repos.AddOneAsync(laptop);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Laptop>> DeleteLaptop(int id)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("not found Producer");
            }

            try
            {
                var del = await _repos.DeleteOneAsync(findId);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("cant delete this producer");
            }
        }
    }
}
