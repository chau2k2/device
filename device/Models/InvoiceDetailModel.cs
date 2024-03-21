using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class InvoiceDetailModel
    {
        
        public int Id { get; set; }
        public ProductType ProductType { get; set; }
        public string ProductName { get; set; }
        public int InvoiceId { get; set; }
        [Range(0, 100000000)]
        [JsonIgnore]
        public decimal Price { get; set; }
        [Range(0, 999)]
        public int Quantity { get; set; }
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
    public enum ProductType
    {
        Laptop,
        PrivateComputer,
        Ram,
        Monitor,
        Vga
    }
}
