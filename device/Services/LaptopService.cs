using device.Data;
using device.IRepository;
using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.IServices;

namespace device.Services
{
    public class LaptopService :ILaptopService
    {
        private readonly ILogger<LaptopService> _logger;
        private readonly IAllRepository<Laptop> _repos;
 
        private readonly LaptopDbContext _context;

        public LaptopService(IAllRepository<Laptop> repos, ILogger<LaptopService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repos = repos;
            _context = context;
        }
        public async Task<TPaging<LaptopResponse>> GetAllLaptop(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<Laptop>().CountAsync();
                int totalPage = (int)Math.Ceiling((double)totalCount / pageSize);

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
                        ProducerName = laptop.Producer!.Name,
                        CostPrice = laptop.CostPrice,
                        SoldPrice = laptop.SoldPrice,
                        ProducerId = laptop.ProducerId
                    });
                }

                return new TPaging<LaptopResponse>
                {
                    numberPage = page,
                    totalRecord = totalCount,
                    Data = laptopResponse
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Laptop>> GetLaptopById(int id)
        {
            try
            {
                var result = await _repos.GetAsyncById(id);
                if (result == null)
                {
                    return new NotFoundResult();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<ActionResult<Laptop>> Updatelaptop(int id, LaptopResponse Upd)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("Not found Producer");
            }

            Laptop laptop = new Laptop()
            {
                Id = id,
                Name = Upd.Name,
                ProducerId = Upd.ProducerId,
                CostPrice = Upd.CostPrice,
                SoldPrice = Upd.SoldPrice
            };
            try
            {
                var result = await _repos.UpdateOneAsyns(laptop);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Laptop>> CreateLaptop(LaptopResponse crl)
        {
            try
            {
                int maxId = await _context.laptops.MaxAsync(p => (int?)p.Id) ?? 0;
                int next = maxId + 1;

                Laptop laptop = new Laptop()
                {
                    Id = next,
                    Name = crl.Name,
                    ProducerId = crl.ProducerId,
                    CostPrice = crl.CostPrice,
                    SoldPrice = crl.SoldPrice
                };

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
                    throw new Exception("Not found Producer");
                }
                laptop.IsDelete = true;
                var del = await _repos.DeleteOneAsync(laptop);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("Can't delete this producer");
            }
        }
    }
}
