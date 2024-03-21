using device.Data;
using device.Entity;
using device.IRepository;
using device.IServices;
using device.ModelResponse;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace device.Services
{
    public class PcService : IPcService
    {
        private readonly IAllRepository<PrivateComputer> _repo;
        private readonly LaptopDbContext _context;

        public PcService(IAllRepository<PrivateComputer> repo, LaptopDbContext context) 
        {
            _repo = repo;
            _context = context;
        }

        public async Task<ActionResult<BaseResponse<PcResponse>>> Create(PrivateComputerModel pcModel)
        {
            try
            {
                PrivateComputer pc = new PrivateComputer()
                {
                    Id = pcModel.Id,
                    Name = pcModel.Name,
                    CostPrice = pcModel.CostPrice,
                    SoldPrice = pcModel.SoldPrice,
                    ProducerId = pcModel.ProducerId,
                    IsDelete = pcModel.IsDelete
                };

                var result = await _repo.AddOneAsync(pc);

                var pcResponse = new PcResponse
                {
                    Id = result.Id,
                    Name = result.Name,
                    CostPrice = result.CostPrice,
                    SoldPrice = result.SoldPrice,
                    ProducerId = result.ProducerId,
                    IsDelete = result.IsDelete,
                    ProducerName = result.Producer.Name
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

        public Task<ActionResult<BaseResponse<PrivateComputer>>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<BaseResponse<PrivateComputer>>> FindPcByName(string name)
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
                        ProducerName = pc.Producer.Name,
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

        public Task<ActionResult<BaseResponse<PrivateComputer>>> Update(PrivateComputerModel pc)
        {
            throw new NotImplementedException();
        }
    }
}
