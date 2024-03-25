using device.Data;
using device.Entity;
using device.IRepository;
using device.IServices;
using device.ModelResponse;
using device.Models;
using device.Response;
using device.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace device.Services
{
    public class PcService : IPcService
    {
        private readonly IAllRepository<PrivateComputer> _repo;
        private readonly LaptopDbContext _context;
        private readonly PcValidate _validate;

        public PcService(IAllRepository<PrivateComputer> repo, LaptopDbContext context) 
        {
            _repo = repo;
            _context = context;
            _validate = new PcValidate(context);
        }

        public async Task<ActionResult<BaseResponse<PcResponse>>> Create(PrivateComputerModel model)
        {
            try
            {
                PrivateComputer pc = new PrivateComputer()
                {
                    Id = model.Id,
                    Name = model.Name,
                    CostPrice = model.CostPrice,
                    SoldPrice = model.SoldPrice,
                    ProducerId = model.ProducerId,
                    IsDelete = model.IsDelete,
                    
                };

                var validator = await _validate.RegexPc(model);

                if (!validator.Success)
                {
                    return new BaseResponse<PcResponse>
                    {
                        Message = validator.Message,
                        ErrorCode = validator.ErrorCode
                    };
                }

                var result = await _repo.AddOneAsync(pc);

                var pcEntity = await _context.Set<PrivateComputer>()
                    .Include(p => p.Producer)
                    .Where( p => p.Id == result.Id)
                    .FirstOrDefaultAsync();

                var pcResponse = new PcResponse
                {
                    Id = result.Id,
                    Name = result.Name,
                    CostPrice = result.CostPrice,
                    SoldPrice = result.SoldPrice,
                    ProducerId = result.ProducerId,
                    IsDelete = result.IsDelete,
                    ProducerName = result.Producer!.Name
                };

                return new BaseResponse<PcResponse>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = pcResponse
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PcResponse>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }

        public async Task<ActionResult<BaseResponse<PrivateComputer>>> Delete(string name)
        {
            try
            {
                var pc = await _context.PrivateComputer.FirstOrDefaultAsync(p => p.Name == name);

                if (pc == null || pc!.IsDelete == true)
                {
                    return new BaseResponse<PrivateComputer>
                    {
                        Success = false,
                        Message = "Not found!!!",
                        ErrorCode = ErrorCode.NotFound
                    };
                }

                pc.IsDelete = true;

                var del = await _repo.UpdateOneAsyns(pc);

                return new BaseResponse<PrivateComputer>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = del
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PrivateComputer>()
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }

        public Task<ActionResult<BaseResponse<PrivateComputer>>> FindPc(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<TPaging<PcResponse>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<PrivateComputer>().CountAsync(l => l.IsDelete == false);

                var result = await _context.Set<PrivateComputer>()!
                    .Include(s => s.Producer)
                    .Where(c => c.IsDelete == false)
                    .Take(pageSize).Skip((page - 1) * pageSize)
                    .ToListAsync();

                List<PcResponse> laptopResponse = new List<PcResponse>();

                foreach (var pc in result)
                {
                    laptopResponse.Add(new PcResponse()
                    {
                        Id = pc.Id,
                        Name = pc.Name,
                        ProducerName = pc.Producer!.Name,
                        CostPrice = pc.CostPrice,
                        SoldPrice = pc.SoldPrice,
                        ProducerId = pc.ProducerId,
                        IsDelete = pc.IsDelete
                    });
                }

                return new TPaging<PcResponse>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = laptopResponse
                };
            }
            catch (Exception ex)
            {
                return new TPaging<PcResponse>
                {
                    Message = ex.Message,
                    Error = Error.Error
                };
            }
        }

        public async Task<ActionResult<BaseResponse<PrivateComputer>>> Update(int id, PrivateComputerModel model)
        {
            try
            {
                var lap = await _repo.GetAsyncById(id);

                if (lap == null || lap!.IsDelete == true)
                {
                    return new BaseResponse<PrivateComputer>
                    {
                        Success = false,
                        Message = "Not found!!!"
                    };
                }

                PrivateComputer pc = new PrivateComputer()
                {
                    Id = id,
                    Name = model.Name,
                    ProducerId = model.ProducerId,
                    CostPrice = model.CostPrice,
                    SoldPrice = model.SoldPrice,
                    IsDelete = model.IsDelete
                };

                var validator = await _validate.RegexPc(model);

                if (!validator.Success)
                {
                    return new BaseResponse<PrivateComputer>
                    {
                        Message = validator.Message,
                        ErrorCode = validator.ErrorCode
                    };
                }

                var result = await _repo.UpdateOneAsyns(pc);

                return new BaseResponse<PrivateComputer>
                {
                    Success = true,
                    Message = "Successfull!!!",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PrivateComputer>
                {
                    Success = false,
                    Message = ex.Message,
                    ErrorCode = ErrorCode.Error
                };
            }
        }
    }
}
