using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string InvoiceNumber {  get; set; }
        public DateTime DateInvoice { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        [JsonIgnore]
        public ICollection<InvoiceDetail> invoiceDetail { get; set;}    
    }
}
