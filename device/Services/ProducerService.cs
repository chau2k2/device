﻿using device.Data;
using device.DTO.Producer;
using device.IRepository;
using device.Models;
using device.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace device.Services
{
    public class ProducerService 
    {
        private readonly ILogger<ProducerService> _logger;
        private readonly IAllRepository<Producer> _repos;
        private readonly ProducerValidate _validate;
        private readonly LaptopDbContext _context;

        public ProducerService(IAllRepository<Producer> repos, ILogger<ProducerService> logger, LaptopDbContext context)
        {
            this._logger = logger;
            _repos = repos;
            _validate = new ProducerValidate();
            _context = context;
        }

        public async Task<IEnumerable<Producer>> GetAll(int page = 1, int pageSize = 5)
        {
            try
            {
                var result = await _repos.GetAllAsync(page, pageSize);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Producer>> GetProducerById( int id)
        {
            var result = await _repos.GetAsyncById(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return result;
        }
        public async Task<ActionResult<Producer>> UpdateProducer (int id, UpdateProducer Upd)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("not found Producer");
            }
            Producer producer = new Producer()
            {
                Id = id,
                Name = Upd.Name,
                IsActive = Upd.IsActive
            };
            try
            {
                var validate = _validate.Validate(producer);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }
                var result = await _repos.UpdateOneAsyns(producer);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Producer>> CreateProducer (CreateProducer cpr)
        {
            int maxId = await _context.producers.MaxAsync(p => (int?)p.Id) ?? 0;
            int next = maxId + 1;

            Producer producer = new Producer()
            {
                Id = next,
                Name = cpr.Name,
                IsActive = cpr.IsActive
            };

            try
            {
                var validate = _validate.Validate(producer);
                if (!validate.IsValid)
                {
                    throw new Exception(string.Join(", ", validate.Errors));
                }
                    var result = await _repos.AddOneAsync(producer);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult<Producer>> DeleteProducer(int id)
        {
            var findId = await _repos.GetAsyncById(id);
            if (findId == null)
            {
                throw new Exception("not found Producer");
            }

            try
            {
                var del = await _repos.DeleteOneAsync(findId);
                return del;
            }
            catch (Exception)
            {
                throw new Exception("cant delete this producer");
            }
        }
    }
}
