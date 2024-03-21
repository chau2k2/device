using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class InvoiceDetailModel
    {
        
        public int Id { get; set; }
        public EProductType ProductType { get; set; }
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
    public enum EProductType
    {
        Laptop = 1,
        PrivateComputer,
        Ram,
        Monitor,
        Vga
    }
}
