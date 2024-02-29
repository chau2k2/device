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
        public string Cpu { get; set; }
        [Required]
        [MaxLength(50)]
        public string Seri { get; set; }

        public int VgaId { get; set; }
        public int RamId { get; set; }
        public string HardDriver { get; set; }
        public int MonitorId { get; set; }
        public string Webcam { get; set; }
        [Range(0, 100)]
        public decimal Weight { get; set; }
        [Range(0, 100)]
        public decimal Height { get; set; }
        [Range(0, 100)]
        public decimal Width { get; set; }
        [Range(0, 100)]
        public decimal Length { get; set; }
        [Range(0, 100)]
        public decimal BatteryCapacity { get; set; }
        [JsonIgnore]
        public virtual Ram Rams { get; set; }

        [JsonIgnore]
        public virtual Vga Vga { get; set; }

        [JsonIgnore]
        public virtual MonitorM Monitor { get; set; }

        public int LaptopId { get; set; }
        public bool IsDelete { get; set; }
        [JsonIgnore]
        public virtual Laptop Laptops { get; set;}
        [JsonIgnore]
        public virtual Storage Storage { get; set; }
    }
}
