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
        Task<ActionResult<BaseResponse<Invoice>>> GetByInvoiceNum(string invoiceNum);
        Task<ActionResult<BaseResponse<Invoice>>> Create(InvoiceModel CrI);
        Task<ActionResult<BaseResponse<Invoice>>> Update(int id, InvoiceModel UpI);
        Task<ActionResult<BaseResponse<Invoice>>> delete(int id);
    }
}
