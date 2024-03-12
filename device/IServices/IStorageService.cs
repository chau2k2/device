using device.Entity;
using device.ModelResponse;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IStorageService
    {
        Task<IEnumerable<Storage>> GetAll(int page, int pageSize);
        Task<ActionResult<Storage>> GetById(int id);
        Task<ActionResult<Storage>> Create(StorageResponse CrS);
        Task<ActionResult<Storage>> Update(int id, StorageResponse UpS);
        Task<ActionResult<Storage>> delete(int id);
    }
}
