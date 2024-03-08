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
                var result = await _context.Set<Laptop>()!
                    .Include(s => s.Producer)
                    .Where(c => c.IsDelete == false)
                    .Take(pageSize).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<LaptopResponse> laptopResponse = new List<LaptopResponse>();

                foreach (var laptop in result)
                {
                    laptopResponse.Add(new LaptopResponse()
                    {
                        Id = laptop.Id,
                        Name = laptop.Name,
                        Profit = laptop.SoldPrice - laptop.CostPrice,
                        ProducerName = laptop.Producer!.Name,
                        CostPrice = laptop.CostPrice
                    });
                }
                return laptopResponse;
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
                ProducerId = Upd.IdProducer,
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
            int maxId = await _context.laptops.MaxAsync(p => (int?)p.Id) ?? 0;
            int next = maxId + 1;

            Laptop laptop = new Laptop()
            {
                Id = next,
                Name = crl.Name,
                ProducerId = crl.IdProducer,
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
            try
            {
                var laptop = await _repos.GetAsyncById(id);
                if (laptop == null)
                {
                    throw new Exception("not found Producer");
                }
                laptop.IsDelete = true;
                var del = await _repos.DeleteOneAsync(laptop);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("cant delete this producer");
            }
        }
    }
}
