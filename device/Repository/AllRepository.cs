using device.Data;
using device.IRepository;
using Microsoft.EntityFrameworkCore;

namespace device.Repository
{
    public class AllRepository<T> : IAllRepository<T> where T : class
    {
        private LaptopDbContext _dbContext;

        public DbSet<T> Entities { get; set; }

        public AllRepository(LaptopDbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<T>();
        }
        public async Task<T> AddOneAsync(T entity)
        {
            await this.Entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteOneAsync(T entity)
        {
            await Task.FromResult<T>(this.Entities.Remove(entity).Entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int page, int pageSize)
        {
            int offset = (page -1) * pageSize;
            return await _dbContext.Set<T>().Skip(offset).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsyncById(int id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<T> UpdateOneAsyns(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
