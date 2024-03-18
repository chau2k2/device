using device.Data;
using device.IRepository;
using device.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using device.ModelResponse;
using device.IServices;
using device.Response;
using device.Validator;
using device.Models;

namespace device.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IAllRepository<Producer> _repos;
        private readonly LaptopDbContext _context;

        public ProducerService(IAllRepository<Producer> repos, LaptopDbContext context)
        {
            _repos = repos;
            _context = context;
        }

        public async Task<TPaging<Producer>> GetAll(int page, int pageSize)
        {
            try
            {
                int totalCount = await _context.Set<Producer>().CountAsync(i => i.IsDelete == false);

                var result = await _repos.GetAllAsync(page, pageSize);

                return new TPaging<Producer>
                {
                    NumberPage = page,
                    TotalRecord = totalCount,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<BaseResponse<Producer>>> GetProducerById( int id)
        {
            try
            {
                var result = await _repos.GetAsyncById(id);

                if (result == null || result.IsDelete == true)
                {
                    return new BaseResponse<Producer>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }
                return new BaseResponse<Producer>
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
        public async Task<ActionResult<BaseResponse<Producer>>> UpdateProducer (int id, ProducerModel Upd)
        {
            try
            {
                var produce = await _repos.GetAsyncById(id);

                if (produce == null || produce.IsDelete == true)
                {
                    return new BaseResponse<Producer>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }
                Producer producer = new Producer()
                {
                    Id = id,
                    Name = Upd.Name,
                    IsActive = Upd.IsActive
                };
            
                var result = await _repos.UpdateOneAsyns(producer);

                return new BaseResponse<Producer>
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
        public async Task<ActionResult<BaseResponse<Producer>>> CreateProducer (ProducerModel cpr)
        {
            try
            {

                int maxId = await _context.producers.MaxAsync(p => (int?)p.Id) ?? 0;

                int next = maxId + 1;

                Producer producer = new Producer()
                {
                    Id = next,
                    Name = cpr.Name,
                    IsActive = cpr.IsActive
                };

                var result = await _repos.AddOneAsync(producer);

                return new BaseResponse<Producer>
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
        public async Task<ActionResult<BaseResponse<Producer>>> DeleteProducer(int id)
        {
            try
            {
                var producer = await _repos.GetAsyncById(id);

                if (producer == null || producer.IsDelete == true)
                {
                    return new BaseResponse<Producer>
                    {
                        Success = false,
                        Message = "NotFound!!!"
                    };
                }
                producer.IsDelete = true;

                var del = await _repos.DeleteOneAsync(producer);

                return new BaseResponse<Producer>
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
