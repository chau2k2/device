using device.Data;
using device.IRepository;
using device.IServices;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;

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
            await _repository.AddOneAsync(entity);
            return entity;
        }

        public async Task<int> Delete<T>(string urlGetById, string urlDel, int id)
        {
            T model = await GetById<T>(urlGetById, id);
            HttpClient client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(model),Encoding.UTF8,"application/json"); // get id from url
            var reponse = await client.GetAsync(urlDel + id); //remove id
            string result = await reponse.Content.ReadAsStringAsync();
            if (!reponse.IsSuccessStatusCode)
            {
                return 0;
            }
            return 1;
        }

        public async Task<IEnumerable<T>> GetAll(int page, int pageSize)
        {
            return await _repository.GetAllAsync(page, pageSize);
        }

        public async Task<T> GetById<T>(string urlGetById, int id)
        {
            var httpClient = new HttpClient();// tao Response from http
            var response = await httpClient.GetAsync(urlGetById + id); // tao url 
            string TResponse = await response.Content.ReadAsStringAsync(); // doc response
            T model = JsonConvert.DeserializeObject<T>(TResponse);
            return model;
        } 

        public async Task<T> Update(int id, T entity)
        {
            await _repository.UpdateOneAsyns(entity);
            return entity;
        }
    }
}
