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
        [Required]
        public int IdVga { get; set; }
        [Required]
        public int IdRam { get; set; }
        [JsonIgnore]
        public Ram Rams { get; set; }
        public string HardDriver { get; set; }
        [Required]
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
        public MonitorM Monitor { get; set; }
        [JsonIgnore]
        public KhoHang KhoHang { get; set; }
        [JsonIgnore]
        [Required]
        public int idLaptop { get; set; }
        [JsonIgnore]
        public Laptop Laptops { get; set;}
    }
}
