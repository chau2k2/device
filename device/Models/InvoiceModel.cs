using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// ngày giờ trong hóa đơn
        /// </summary>
        public DateTime DateInvoice { get; set; }
        /// <summary>
        /// trường xóa => xóa mềm ( soft delete)
        /// </summary>
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
