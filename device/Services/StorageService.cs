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
                int totalCount = await _context.Set<Storage>().CountAsync(i => i.IsDelete == false);

                var result = await _context.Set<Storage>()
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
                        ProductType = (EProductType)storage.ProductType,
                        Invenetory = storage.inventory,
                        SoldNumber = storage.SoldNumber,
                        ProductName = storage.ProductName,
                        IsDelete = storage.IsDelete
                    });
                }

                return new TPaging<StorageResponse>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = storageResponses
                };
            }
            catch (Exception ex)
            {
                return new TPaging<StorageResponse>
                {
                    Message = ex.Message,
                    Error = Error.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Storage>>> GetById(int id)
        {
            try
            {
                var storage = await _repo.GetAsyncById(id);

                if (storage == null || storage.IsDelete == true)
                {
                    return new BaseResponse<Storage>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }
                return new BaseResponse<Storage>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = storage
                };
            }
            catch (Exception ex) 
            {
                return new BaseResponse<Storage>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Storage>>> Create(StorageModel CrS)
        {
            try
            {
                int maxId = await _context.storages.MaxAsync(r => (int?)r.Id) ?? 0;
                int nextId = maxId + 1;

                Storage storage = new Storage()
                {
                    Id = nextId,
                    ProductType = (int) CrS.ProductType,
                    ProductName = CrS.ProductName,
                    inventory = CrS.ImportNumber - CrS.SoldNumber,
                    ImportNumber = CrS.ImportNumber,
                    SoldNumber = CrS.SoldNumber
                };

                switch (storage.ProductType)
                {
                    case 1:
                        var laptop = await _context.laptops.FirstOrDefaultAsync(s => s.Name == CrS.ProductName);

                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                }

                var result = await _repo.AddOneAsync(storage);

                return new BaseResponse<Storage>
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
        public async Task<ActionResult<BaseResponse<Storage>>> Update(int id, StorageModel UpS)
        {
            try
            {
                var storages = await _context.storages.FindAsync(id);

                if (storages == null || storages.IsDelete == true)
                {
                    return new NotFoundResult();
                }

                Storage storage = new Storage()
                {
                    Id = id,
                   // LaptopId = UpS.LaptopId,
                    ImportNumber = UpS.ImportNumber,
                    SoldNumber = UpS.SoldNumber
                };

                var result = await _repo.UpdateOneAsyns(storages);

                return new BaseResponse<Storage>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Storage>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Storage>>> Delete(int id)
        {
            try
            {
                var storage = await _repo.GetAsyncById(id);

                if (storage == null)
                {
                    return new BaseResponse<Storage>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }

                storage.IsDelete = true;

                var del = await _repo.DeleteOneAsync(storage);

                return new BaseResponse<Storage>
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
