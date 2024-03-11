using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Entity
{
    public class Ram 
    {
        /// <summary>
        /// id Ram
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// tên ram - kiểu số
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// giá của ram
        /// </summary>
        [Range(0, 10000000)]
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
