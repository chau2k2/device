using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;
using device.Response;
using device.Models;
using device.Validator;
using System.ComponentModel.DataAnnotations;

namespace device.Services
{
    public class StorageService : IStorageService
    {
        private readonly IAllRepository<Storage> _repo;
        private readonly LaptopDbContext _context;
        private readonly StorageValidate _validate;

        public StorageService(IAllRepository<Storage> repo,LaptopDbContext context)
        {
            _repo = repo;
            _context = context;
            _validate = new StorageValidate(context);
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
                        ProductType = storage.ProductType,
                        Invenetory = storage.inventory,
                        SoldNumber = storage.SoldNumber,
                        ProductId = storage.ProductId,
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
        public async Task<ActionResult<BaseResponse<Storage>>> Create(StorageModel model)
        {
            try
            {
                int maxId = await _context.storages.MaxAsync(r => (int?)r.Id) ?? 0;
                int nextId = maxId + 1;

                Storage storage = new Storage()
                {
                    Id = nextId,
                    ProductType = model.ProductType,
                    ProductId = model.ProductId,
                    inventory = model.ImportNumber - model.SoldNumber,
                    ImportNumber = model.ImportNumber,
                    SoldNumber = model.SoldNumber
                };

                var validator = await _validate.RegexStorage(model);

                if (!validator.Success)
                {
                    return new BaseResponse<Storage>
                    {
                        Message = validator.Message,
                        ErrorCode = validator.ErrorCode
                    };
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
                return new BaseResponse<Storage>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
        public async Task<ActionResult<BaseResponse<Storage>>> Update(int id, StorageModel model)
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
                    ProductType = model.ProductType,
                    ProductId = model.ProductId,
                    inventory = model.ImportNumber - model.SoldNumber,
                    ImportNumber = model.ImportNumber,
                    SoldNumber = model.SoldNumber
                };

                var validator = await _validate.RegexStorage(model);

                if (!validator.Success)
                {
                    return new BaseResponse<Storage>
                    {
                        Message = validator.Message,
                        ErrorCode = validator.ErrorCode
                    };
                }

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
                return new BaseResponse<Storage>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
    }
}
