using device.Models;

namespace device.Response
{
    public class InvoiceDetailResponse : InvoiceDetailModel
    {
        public string? InvoiceNumber { get; set; }
        public string? LaptopName { get; set; }
    }
}
