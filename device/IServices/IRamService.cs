using device.Entity;
using device.ModelResponse;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IRamService
    {
        Task<IEnumerable<Ram>> GetAll(int page, int pageSize);
        Task<ActionResult<Ram>> GetById(int id);
        Task<ActionResult<Ram>> Create(RamResponse CrR);
        Task<ActionResult<Ram>> Update(int id, RamResponse UpR);
        Task<ActionResult<Ram>> delete(int id);
    }
}
