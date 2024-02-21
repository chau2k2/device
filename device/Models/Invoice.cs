using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string? InvoiceNumber {  get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime DateInvoice { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        [JsonIgnore]
        public ICollection<InvoiceDetail>? invoiceDetail { get; set;}
        public Invoice()
        {
            GererateInvoiceNumber();
        }
        private void GererateInvoiceNumber()
        {
            InvoiceNumber = "IV" + Id.ToString("D4");
        }
    }
}
