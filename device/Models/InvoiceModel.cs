using System.Text.Json.Serialization;

namespace device.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        /// <summary>
        /// số hóa đơn
        /// </summary>
        [JsonIgnore]
        public string? InvoiceNumber { get; set; }

        /// <summary>
        /// trường xóa => xóa mềm ( soft Delete)
        /// </summary>
        [JsonIgnore]
        public bool IsDelete { get; set; }
        public InvoiceDetailModel Details { get; set; }
    }
}
