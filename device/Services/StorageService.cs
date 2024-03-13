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
    public class StorageService : IStorageService
    {
        private readonly IAllRepository<Storage> _repo;
        private readonly LaptopDbContext _context;

        public StorageService(IAllRepository<Storage> repo,LaptopDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        public async Task<TPaging<StorageResponse>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<Storage>().CountAsync();

                var result = await _context.Set<Storage>()
                    .Include(s => s.LaptopDetail)
                        .ThenInclude(s => s.Laptops)
                    .Where( s => s.IsDelete == false)
                    .Take(pageSize).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<StorageResponse> storageResponses = new List<StorageResponse>();

                foreach (var storage in result)
                {
                    storageResponses.Add(new StorageResponse()
                    {
                        Id = storage.Id,
                        ImportNumber = storage.ImportNumber,
                        LaptopDetailId = storage.LaptopDetailId,
                        SoldNumber = storage.SoldNumber,
                        nameLaptop = storage.LaptopDetail.Laptops.Name,
                        IsDelete = storage.IsDelete
                    });
                }

                return new TPaging<StorageResponse>
                {
                    numberPage = page,
                    totalRecord = totalCount,
                    Data = storageResponses
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Storage>>> GetById(int id)
        {
            try
            {
                var result = await _repo.GetAsyncById(id);

                if (result == null)
                {
                    return new BaseResponse<Storage>
                    {
                        success = false,
                        message = "Not found!!!"
                    };
                }
                return new BaseResponse<Storage>
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
        public async Task<ActionResult<BaseResponse<Storage>>> Create(StorageResponse CrS)
        {
            try
            {
                int maxId = await _context.storages.MaxAsync(r => (int?)r.Id) ?? 0;
                int nextId = maxId + 1;

                Storage storage = new Storage()
                {
                    Id = nextId,
                    LaptopDetailId = CrS.LaptopDetailId,
                    ImportNumber = CrS.ImportNumber,
                    SoldNumber = CrS.SoldNumber
                };

                var result = await _repo.AddOneAsync(storage);

                return new BaseResponse<Storage>
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
        public async Task<ActionResult<BaseResponse<Storage>>> Update(int id, StorageResponse UpS)
        {
            try
            {
                var findId = await _context.ram.FindAsync(id);

                if (findId == null)
                {
                    return new NotFoundResult();
                }

                Storage storage = new Storage()
                {
                    Id = id,
                    LaptopDetailId = UpS.LaptopDetailId,
                    ImportNumber = UpS.ImportNumber,
                    SoldNumber = UpS.SoldNumber
                };

                var result = await _repo.UpdateOneAsyns(storage);

                return new BaseResponse<Storage>
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
        public async Task<ActionResult<BaseResponse<Storage>>> delete(int id)
        {
            try
            {
                var storage = await _repo.GetAsyncById(id);

                if (storage == null)
                {
                    return new BaseResponse<Storage>
                    {
                        success = false,
                        message = "Not found!!!"
                    };
                }

                storage.IsDelete = true;

                var del = await _repo.DeleteOneAsync(storage);

                return new BaseResponse<Storage>
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
