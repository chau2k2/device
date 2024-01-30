using device.Data;
using device.IRepository;
using device.IServices;

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

        public async Task<bool> CheckConstraint(int id)
        {
            var checkIdLaptop = _dbContext.laptops.Any(x => x.Id == id);
            var checkIdLapDetail = _dbContext.laptopsDetail.Any(x => x.Id == id);
            var checkidProducer = _dbContext.producers.Any(x => x.Id == id);
            var checkIdRam = _dbContext.ram.Any(x => x.Id == id);
            var chechIdVga = _dbContext.vgas.Any(x => x.Id == id);
            var checkIdMonitor = _dbContext.monitors.Any(x => x.Id == id);
            var checkIdKhoHang = _dbContext.khoHangs.Any(x => x.Id == id);
            return checkIdLaptop && checkIdLapDetail && checkidProducer && checkIdRam && chechIdVga && checkIdMonitor && checkIdKhoHang;
        }

        public async Task<T> Delete(int id)
        {
            var checkIdLaptop = _dbContext.laptops.Any(x => x.Id == id);
            var checkIdLapDetail = _dbContext.laptopsDetail.Any(x => x.Id == id);
            var checkidProducer = _dbContext.producers.Any(x => x.Id == id);
            var checkIdRam = _dbContext.ram.Any(x => x.Id == id);
            var chechIdVga = _dbContext.vgas.Any(x => x.Id == id);
            var checkIdMonitor = _dbContext.monitors.Any(x => x.Id == id);
            var checkIdKhoHang = _dbContext.khoHangs.Any(x => x.Id == id);
            if (checkIdLaptop && checkIdLapDetail && checkidProducer && checkIdRam && chechIdVga && checkIdMonitor && checkIdKhoHang)
            {
                throw new Exception("can not delete this entity");
            }
            var idDel = await _repository.GetAsyncById(id);
            if (idDel != null)
            {
                await _repository.DeleteOneAsync(idDel);
            }
            return idDel;
        }

        public async Task<IEnumerable<T>> GetAll(int page, int pageSize)
        {
            return await _repository.GetAllAsync(page, pageSize);
        }

        public async Task<T> GetById(int id)
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
