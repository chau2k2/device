using device.Entity;
using device.ModelResponse;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetAll(int page, int pageSize);
        Task<ActionResult<Invoice>> GetByInvoiceNum(string invoiceNum);
        Task<ActionResult<Invoice>> Create(InvoiceResponse CrI);
        Task<ActionResult<Invoice>> Update(int id, InvoiceResponse UpI);
        Task<ActionResult<Invoice>> delete(int id);
    }
}
