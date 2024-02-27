using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int IdProducer { get; set; }
        public decimal CostPrice { get; set; }
        [Required]
        [Range(0, 10000000)]
        public decimal SoldPrice { get; set; }
        [JsonIgnore]     
        public Producer? producer { get; set; }
        [JsonIgnore]
        public ICollection<LaptopDetail> LaptopDetails { get; set; }
        [JsonIgnore]
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
