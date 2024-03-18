using device.Data;
using device.IRepository;
using device.Entity;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.IServices;
using device.Models;

namespace device.Services
{
    public class LaptopService :ILaptopService
    {
        private readonly IAllRepository<Laptop> _repos;
        private readonly LaptopDbContext _context;

        public LaptopService(IAllRepository<Laptop> repos, LaptopDbContext context)
        {
            _repos = repos;
            _context = context;
        }

        public async Task<TPaging<LaptopResponse>> GetAllLaptop(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<Laptop>().CountAsync(l => l.IsDelete == false);

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
                        ProducerName = laptop.Producer?.Name,
                        CostPrice = laptop.CostPrice,
                        SoldPrice = laptop.SoldPrice,
                        ProducerId = laptop.ProducerId,
                        inventory = laptop.inventory,
                        IsDelete = laptop.IsDelete
                    }) ;
                }

                return new TPaging<LaptopResponse>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = laptopResponse
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult<BaseResponse<Laptop>>> GetLaptopById(int id)
        {
            try
            {
                var result = await _repos.GetAsyncById(id);

                if (result == null || result!.IsDelete == true)
                {
                    return new BaseResponse<Laptop>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }

                return new BaseResponse<Laptop>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<ActionResult<BaseResponse<Laptop>>> Updatelaptop(int id, LaptopModel Upd)
        {
            try
            {
                var lap = await _repos.GetAsyncById(id);

                if (lap == null || lap!.IsDelete == true)
                {
                    return new BaseResponse<Laptop>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }

                Laptop laptop = new Laptop()
                {
                    Id = id,
                    Name = Upd.Name,
                    ProducerId = Upd.ProducerId,
                    CostPrice = Upd.CostPrice,
                    SoldPrice = Upd.SoldPrice,
                    IsDelete = Upd.IsDelete
                };
            
                var result = await _repos.UpdateOneAsyns(laptop);

                return new BaseResponse<Laptop>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Laptop>>> CreateLaptop( LaptopModel crl)
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
                return new BaseResponse<Laptop> 
                { 
                    Success = true, 
                    Message = "Successfull!!!", 
                    Data = result 
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Laptop>>> DeleteLaptop(int id)
        {
            try
            {
                var laptop = await _repos.GetAsyncById(id);

                if (laptop == null || laptop!.IsDelete == true)
                {
                    return new BaseResponse<Laptop>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }

                laptop.IsDelete = true;

                var del = await _repos.DeleteOneAsync(laptop);

                return new BaseResponse<Laptop>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = del
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
