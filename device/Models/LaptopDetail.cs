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
        public string Cpu {  get; set; }
        [Required]
        public string Seri { get; set; }
        [Required,ForeignKey("cvga")]
        public int IdVga { get; set; }
        [Required, ForeignKey("cram")]
        public int IdRam { get; set; }
        public string HardDriver { get; set; }
        [Required,ForeignKey("monitor")]
        public int IdMonitor { get; set; }
        public string Webcam { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public string BatteryCatttery { get; set; }
        [ForeignKey("khohang")]
        public int IdKhoHang { get; set; }
        public byte[] Image { get; set; }

        [JsonIgnore]
        public Vga Vga { get; set; }
        [JsonIgnore]
        public Ram Ram { get; set; }
        [JsonIgnore]
        public MonitorM Monitor { get; set; }
        [JsonIgnore]
        public KhoHang KhoHang { get; set; }
        [JsonIgnore]
        public ICollection<Laptop> laptops { get; set; }

    }
}
