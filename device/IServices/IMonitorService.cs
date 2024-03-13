using device.Entity;
using device.ModelResponse;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IMonitorService
    {
        Task<TPaging<MonitorM>> GetAll(int page, int pageSize);
        Task<ActionResult<BaseResponse<MonitorM>>> GetById(int id);
        Task<ActionResult<BaseResponse<MonitorM>>> Update(int id, MonitorResponse UpM);
        Task<ActionResult<BaseResponse<MonitorM>>> Create(MonitorResponse CrM);
        Task<ActionResult<BaseResponse<MonitorM>>> Delete(int id);
    }
}
