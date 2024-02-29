using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string InvoiceNumber {  get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime DateInvoice { get; set; }
        [Range(0,10000)]
        public int TotalQuantity { get; set; }
        [Range(0,1000000000)]
        public decimal TotalPrice { get; set; }
        public bool IsDelete { get; set; }
        [JsonIgnore]
        public ICollection<InvoiceDetail>? invoiceDetail { get; set;}
    }
}
