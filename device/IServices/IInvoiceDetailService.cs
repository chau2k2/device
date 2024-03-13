using device.Entity;
using device.ModelResponse;
using device.Response;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IInvoiceDetailService
    {
        Task<IEnumerable<InvoiceDetailResponse>> GetAllInvoiceDetail(int page, int pageSize);
        Task<ActionResult<InvoiceDetail>> CreateInvoiceDetail(InvoiceDetailResponse CID);
        Task<ActionResult<InvoiceDetail>> Delete(int id);
        Task<IEnumerable<InvoiceDetail>> findInvoiceDetailByINumber(string invoiceNumber);
    }
}
