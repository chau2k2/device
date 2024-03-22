using device.Entity;
using device.ModelResponse;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IProducerService
    {
        Task<TPaging<Producer>> GetAll(int page = 1, int pageSize = 5);
        Task<ActionResult<BaseResponse<Producer>>> GetProducerById(int id);
        Task<ActionResult<BaseResponse<Producer>>> UpdateProducer(int id, ProducerModel model);
        Task<ActionResult<BaseResponse<Producer>>> CreateProducer(ProducerModel model);
        Task<ActionResult<BaseResponse<Producer>>> DeleteProducer(int id);
    }
}
