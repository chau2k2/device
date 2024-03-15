using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Entity
{
    public class LaptopDetail
    {
        /// <summary>
        /// Id Chi tiết Laptop
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// cpu của laptop
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Cpu { get; set; }
        /// <summary>
        /// seri - chuỗi số duy nhất
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Seri { get; set; }
        /// <summary>
        /// web cam của laptop
        /// </summary>
        public string Webcam { get; set; }
        /// <summary>
        /// cân nặng
        /// </summary>
        [Range(0, 100)]
        public decimal Weight { get; set; }
        /// <summary>
        /// chiều cao
        /// </summary>
        [Range(0, 100)]
        public decimal Height { get; set; }
        /// <summary>
        /// chiều rộng
        /// </summary>
        [Range(0, 100)]
        public decimal Width { get; set; }
        /// <summary>
        /// chiều dài
        /// </summary>
        [Range(0, 100)]
        public decimal Length { get; set; }
        /// <summary>
        /// dung lượng pin
        /// </summary>
        [Range(0, 100)]
        public decimal BatteryCapacity { get; set; }
        /// <summary>
        /// ổ cứng
        /// </summary>
        public string HardDriver { get; set; }
        /// <summary>
        /// khóa ngoại liên kết với bảng vga ( Card đồ họa)
        /// </summary>
        public int VgaId { get; set; }
        /// <summary>
        /// khóa ngoại liên kết với bảng Ram
        /// </summary>
        public int RamId { get; set; }
        /// <summary>
        /// khóa ngoại liên kết với bảng Monitor
        /// </summary>
        public int MonitorId { get; set; }
        /// <summary>
        /// khóa ngoại liên kết với bảng laptop
        /// </summary>
        public int LaptopId { get; set; }
        /// <summary>
        /// trường xóa mềm
        /// </summary>
        [JsonIgnore] 
        public bool IsDelete { get; set; }
        /// <summary>
        /// liên kết với bảng Ram
        /// </summary>
        [JsonIgnore]
        public virtual Ram Rams { get; set; }
        /// <summary>
        /// liên kết n - 1 với bảng vga
        /// </summary>
        [JsonIgnore]
        public virtual Vga Vga { get; set; }
        /// <summary>
        /// liên kết n - 1 với bảng monitor
        /// </summary>
        [JsonIgnore]
        public virtual MonitorM Monitor { get; set; }
        /// <summary>
        /// liên kết n -1 với bảng laptop
        /// </summary>
        [JsonIgnore]
        public virtual Laptop Laptops { get; set;}

    }
}
