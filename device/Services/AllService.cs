﻿using device.Data;
using device.IRepository;
using device.IServices;
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

        public async Task<int> Delete(int id)
        {
            return 0;
            
        }

        public async Task<IEnumerable<T>> GetAll(int page, int pageSize)
        {
            return await _repository.GetAllAsync(page, pageSize);
        }

        public async Task<T> GetById<T>( string url)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            string TReponse = await response.Content.ReadAsStringAsync();
            T model = JsonConvert.DeserializeObject<T>(TReponse);
            return model;
        }

        public async Task<T> Test(int id)
        {
            return await _repository.GetAsyncById(id);
        }

        public async Task<T> Update(int id, T entity)
        {
            await _repository.UpdateOneAsyns(entity);
            return entity;
        }
    }
}
