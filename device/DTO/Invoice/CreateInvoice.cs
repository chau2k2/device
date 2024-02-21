using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace device.DTO.HoaDon
{
    public class CreateInvoice
    {
        public string? InvoiceNumber { get; set; }
        public DateTime DateInvoice { get; set; }
    }
}
