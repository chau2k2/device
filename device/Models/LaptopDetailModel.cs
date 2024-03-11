using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class LaptopDetailModel
    {
        /// <summary>
        /// Id Chi tiết Laptop
        /// </summary>
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
        public bool IsDelete { get; set; }
    }
}
