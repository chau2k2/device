using device.Data;
using device.IRepository;
using device.IServices;
using device.Models;
using Humanizer.Localisation.TimeToClockNotation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace device.Services
{
    public class AllService<T> : IAllService<T> where T : class
    {
        private readonly IAllRepository<T> _repository;
        private LaptopDbContext _dbContext;
        public AllService( IAllRepository<T> repository, LaptopDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            return await _repository.AddOneAsync(entity);  
        }

        public async Task<T> Delete(int id)
        {
            var Del = await _repository.GetAsyncById(id);
            await _repository.DeleteOneAsync(Del);
            return Del;
        }

        public async Task<IEnumerable<T>> GetAll(int page, int pageSize)
        {
            return await _repository.GetAllAsync(page, pageSize);
        }

        public async Task<T> GetById( int id)
        {
            return await _repository.GetAsyncById(id);
        }
        public async Task<T> Update(int id, T entity)
        {
            await _repository.UpdateOneAsyns(entity);
            return entity;
        }
        //check trong laptop co idproducer khong
        public async Task<bool> CheckIdProducer_Laptop(int id)
        {
            return _dbContext.laptops.Any(l => l.IdProducer == id);
        }
        //check trong bang producer da co id chua => check constraint laptop va producer
        public async Task<bool> CheckIdProducerOfProducer(int id)
        {
            return _dbContext.producers.Any(i => i.Id == id);
        }
        //check trong bang LaptopDetail co Idlap khong
        public async Task<bool> CheckIdLaptop_LaptopDetail(int id)
        {
            return _dbContext.laptopsDetail.Any(l => l.idLaptop == id);
        }
        //check bang laptop id da ton tai chua
        public async Task<bool> CheckIdLaptop(int id)
        {
            return _dbContext.laptops.Any(l => l.Id == id);
        }
    }
}
