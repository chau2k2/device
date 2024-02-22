using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int Id { get; set; }
        public int IdLaptop { get; set; }
        public int IdInvoice { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public virtual Invoice invoices { get; set; }
        [JsonIgnore]
        public virtual Laptop Laptop { get; set; }
    }
}
