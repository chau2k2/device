using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;
using device.Response;

namespace device.Services
{
    public class RamService : IRamService
    {
        private readonly IAllRepository<Ram> _repo;
        private readonly LaptopDbContext _context;

        public RamService( IAllRepository<Ram> repo, LaptopDbContext context) 
        {
            _repo = repo;
            _context = context;
        }

        public async Task<TPaging<Ram>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<Ram>().CountAsync();

                var result = await _repo.GetAllAsync(page, pageSize);

                return new TPaging<Ram>
                {
                    numberPage = page,
                    totalRecord = totalCount,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Ram>>> GetById (int id)
        {
            try
            {
                var result = await _repo.GetAsyncById(id);
                if (result == null)
                {
                    return new BaseResponse<Ram>
                    {
                        success = false,
                        message = "NotFound!!!"
                    };
                }
                return new BaseResponse<Ram>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Ram>>> Create (RamResponse CrR)
        {
            try
            {
                int maxId = await _context.ram.MaxAsync(r => (int?)r.Id) ?? 0;
                int nextId = maxId + 1;

                Ram ram = new Ram()
                {
                    Id = nextId,
                    Name = CrR.Name,
                    Price = CrR.Price
                };

                var result = await _repo.AddOneAsync(ram);

                return new BaseResponse<Ram>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Ram>>> Update (int id, RamResponse UpR)
        {
            try
            {
                var findId = await _context.ram.FindAsync(id);

                if (findId == null)
                {
                    return new BaseResponse<Ram>
                    {
                        success = false,
                        message = "NotFound!!!"
                    };
                }

                Ram ram = new Ram()
                {
                    Id = id,
                    Name = UpR.Name,
                    Price= UpR.Price
                };
            
                var result = await _repo.UpdateOneAsyns(ram);

                return new BaseResponse<Ram>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Ram>>> delete (int id)
        {
            try
            {
                var ram = await _repo.GetAsyncById(id);

                if (ram == null)
                {
                    return new BaseResponse<Ram>
                    {
                        success = false,
                        message = "NotFound!!!"
                    };
                }

                ram.IsDelete = true;

                var del = await _repo.DeleteOneAsync(ram);

                return new BaseResponse<Ram>
                {
                    success = true,
                    message = "Successfull!!!",
                    data = del
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
