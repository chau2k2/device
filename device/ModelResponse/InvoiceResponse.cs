using device.Models;
using device.Response;

namespace device.ModelResponse
{
    public class InvoiceResponse : InvoiceModel
    {
        /// <summary>
        /// ngày giờ trong hóa đơn
        /// </summary>
        public DateTime DateInvoice { get; set; }
        public List< InvoiceDetailResponse> InvoiceDetail { get; set; }
    }
}
