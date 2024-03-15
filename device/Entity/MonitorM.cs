using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Entity
{
    public class MonitorM
    {
        /// <summary>
        /// id Màn 
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// tên màn
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// giá của màn hình ( monitor)
        /// </summary>
        [Range(0, 100000000)]
        public decimal Price { get; set; }
        /// <summary>
        /// trường xóa => xóa mềm
        /// </summary>
        [JsonIgnore] 
        public bool IsDelete { get; set; }
        /// <summary>
        /// liên kết 1 - n với bảng laptop Detail
        /// </summary>
        [JsonIgnore]
        public ICollection<LaptopDetail> LaptopDetail { get; set;}
    }
}
