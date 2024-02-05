using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class KhoHang
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("fkDetail")]
        public int idDetail { get; set; }
        [Required]
        public int SoLuongBan { get; set; }
        [Required]
        public int SoLuongNhap { get; set; }
        
        [JsonIgnore]
        public LaptopDetail laptopDetail { get; set; }
    }
}
