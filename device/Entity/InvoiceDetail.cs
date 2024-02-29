using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int Id { get; set; }
        public int LaptopId { get; set; }
        public int InvoiceId { get; set; }
        public decimal Price { get; set; }
        [Range(0,1000)]
        public int Quantity { get; set; }
        public bool IsDelete { get; set; }
        [JsonIgnore]
        public virtual Invoice invoices { get; set; }
        [JsonIgnore]
        public virtual Laptop Laptop { get; set; }
    }
}
