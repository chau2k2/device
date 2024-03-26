using device.Entity;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string? InvoiceNumber { get; set; }
        /// <summary>
        /// ngày giờ trong hóa đơn
        /// </summary>
        public DateTime DateInvoice { get; set; }

        public EProductType ProductType { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
        /// <summary>
        /// trường xóa => xóa mềm ( soft Delete)
        /// </summary>
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
