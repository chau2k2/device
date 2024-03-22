using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;
using device.Response;
using device.Models;

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
                int totalCount = await _context.Set<Ram>().CountAsync(i => i.IsDelete == false);

                var result = await _repo.GetAllAsync(page, pageSize);

                return new TPaging<Ram>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new TPaging<Ram>
                {
                    Message = ex.Message,
                    Error = Error.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Ram>>> GetById (int id)
        {
            try
            {
                var result = await _repo.GetAsyncById(id);
                if (result == null || result.IsDelete == true)
                {
                    return new BaseResponse<Ram>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }
                return new BaseResponse<Ram>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Ram>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Ram>>> Create (RamModel CrR)
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
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Ram>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Ram>>> Update (int id, RamModel UpR)
        {
            try
            {
                var ram_id = await _context.ram.FindAsync(id);

                if (ram_id == null || ram_id.IsDelete == true)
                {
                    return new BaseResponse<Ram>
                    {
                        Success = false,
                        Message = "NotFound!!!"
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
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Ram>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Ram>>> Delete (int id)
        {
            try
            {
                var ram = await _repo.GetAsyncById(id);

                if (ram == null || ram.IsDelete == false)
                {
                    return new BaseResponse<Ram>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }

                ram.IsDelete = true;

                var del = await _repo.DeleteOneAsync(ram);

                return new BaseResponse<Ram>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = del
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Ram>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
    }
}
