using device.Entity;
using device.ModelResponse;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IMonitorService
    {
        Task<IEnumerable<MonitorM>> GetAll(int page, int pageSize);
        Task<ActionResult<MonitorM>> GetById(int id);
        Task<ActionResult<MonitorM>> Update(int id, MonitorResponse UpM);
        Task<ActionResult<MonitorM>> Create(MonitorResponse CrM);
        Task<ActionResult<MonitorM>> Delete(int id);
    }
}
