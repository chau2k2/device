using device.Entity;
using device.ModelResponse;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IMonitorService
    {
        Task<TPaging<MonitorM>> GetAll(int page, int pageSize);
        Task<ActionResult<BaseResponse<MonitorM>>> GetById(int id);
        Task<ActionResult<BaseResponse<MonitorM>>> Update(int id, MonitorModel model);
        Task<ActionResult<BaseResponse<MonitorM>>> Create(MonitorModel model);
        Task<ActionResult<BaseResponse<MonitorM>>> Delete(int id);
    }
}
