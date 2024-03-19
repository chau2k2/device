using device.Models;
using device.Response;

namespace device.ModelResponse
{
    public class InvoiceResponse : InvoiceModel
    {
        public List< InvoiceDetailResponse> InvoiceDetail { get; set; }
    }
}
