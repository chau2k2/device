using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class KhoHang
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SoLuongBan { get; set; }
        [Required]
        public int SoLuongNhap { get; set; }
        [Required]
        public double GiaVon {  get; set; }
        [Required]
        public double Giaban { get; set; }
        [JsonIgnore]
        public ICollection<LaptopDetail> LaptopDetail { get; set;}
    }
}
