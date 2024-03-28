using device.Entity;
using device.ModelResponse;
using device.Models;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IInvoiceService
    {
        Task<TPaging<InvoiceResponse>> GetAll(int page, int pageSize);
        Task<ActionResult<BaseResponse<InvoiceResponse>>> GetById(int id);
        Task<ActionResult<BaseResponse<Invoice>>> Create(InvoiceModel model);
        Task<ActionResult<BaseResponse<Invoice>>> Delete(int id);
    }
}
