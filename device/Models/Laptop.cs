using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //foreign key for Laptop
        [Required]
        public int IdProducer { get; set; }
        [Required]
        public double CostPrice { get; set; }
        [Required]
        public double SoldPrice { get; set; }
        [JsonIgnore]     
        public Producer? producer { get; set; }
        [JsonIgnore]
        public ICollection<LaptopDetail> LaptopDetails { get; set; }
        [JsonIgnore]
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
