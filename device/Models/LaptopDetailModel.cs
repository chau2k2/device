using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class LaptopDetailModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Cpu { get; set; }
        [Required]
        [MaxLength(50)]
        public string Seri { get; set; }
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
        public string HardDriver { get; set; }
        public int VgaId { get; set; }
        public int RamId { get; set; }
        public int MonitorId { get; set; }
        public int LaptopId { get; set; }
        [JsonIgnore] 
        public bool IsDelete { get; set; }
    }
}
