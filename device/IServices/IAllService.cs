namespace device.IServices
{
    public interface IAllService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(int page, int pageSize);
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(int id, T entity);
        Task<T> Delete(int id);
    }
}
