using device.Entity;
using device.ModelResponse;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IProducerService
    {
        Task<IEnumerable<Producer>> GetAll(int page = 1, int pageSize = 5);
        Task<ActionResult<Producer>> GetProducerById(int id);
        Task<ActionResult<Producer>> UpdateProducer(int id, ProducerResponse Upd);
        Task<ActionResult<Producer>> CreateProducer(ProducerResponse cpr);
        Task<ActionResult<Producer>> DeleteProducer(int id);
    }
}
