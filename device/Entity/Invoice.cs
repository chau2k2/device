using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Entity
{
    public class Invoice
    {
        /// <summary>
        /// Id Hóa đơn
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// số hóa đơn ( duy nhất) vd: IV00001
        /// </summary>
        public string InvoiceNumber {  get; set; }
        /// <summary>
        /// ngày giờ trong hóa đơn
        /// </summary>
        [Column(TypeName = "timestamp without time zone")]
        public DateTime DateInvoice { get; set; }
        /// <summary>
        /// trường xóa => xóa mềm ( soft Delete)
        /// </summary>
        [JsonIgnore]
        public bool IsDelete { get; set; }
        /// <summary>
        /// liên kết với chi tiết hóa đơn ( invoice Detail)
        /// </summary>
        [JsonIgnore]
        public ICollection<InvoiceDetail>? invoiceDetail { get; set;}
    }
}
