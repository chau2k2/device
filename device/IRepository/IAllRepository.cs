using Microsoft.EntityFrameworkCore;

namespace device.IRepository
{
    public interface IAllRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Entities { get; set; } //DbSet tong
        Task<IEnumerable<TEntity>> GetAllAsync(int page, int pageSize); // get all in pagination
        Task<TEntity> GetAsyncById(int id); // lay 1 phan tu theo id
        Task<TEntity> AddOneAsync (TEntity entity); // add 1 phan tu
        Task<TEntity> UpdateOneAsyns(TEntity entity ); // update 1 
        Task<TEntity> DeleteOneAsync(TEntity entity);// delete 1

    }
}
