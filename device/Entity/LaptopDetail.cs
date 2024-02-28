using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class LaptopDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Cpu {  get; set; }
        [Required]
        [MaxLength(50)]
        public string Seri { get; set; }

        public int IdVga { get; set; }
        public int IdRam { get; set; }
        public string HardDriver { get; set; }
        public int IdMonitor { get; set; }
        public string Webcam { get; set; }
        [Range ( 0, 100)]
        public decimal Weight { get; set; }
        [Range(0, 100)]
        public decimal Height { get; set; }
        [Range(0, 100)]
        public decimal Width { get; set; }
        [Range(0, 100)]
        public decimal Length { get; set; }
        [Range(0, 100)]
        public decimal BatteryCapacity { get; set; }

        public int idLaptop { get; set; }
        public Ram Rams { get; set; }

        [JsonIgnore]
        public virtual Vga Vga { get; set; }
        
        [JsonIgnore]
        public virtual MonitorM Monitor { get; set; }
        
        [JsonIgnore]
        [ForeignKey("Laptop")]
        public virtual Laptop Laptops { get; set;}
        [JsonIgnore]
        [ForeignKey("Storage")]
        public virtual Storage storage { get; set; }
    }
}
