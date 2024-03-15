using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Entity
{
    public class Vga
    {
        /// <summary>
        /// Id Card đồ họa
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// tên card đồ họa
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// giá
        /// </summary>
        [Range(0, 10000000)]
        public decimal Price { get; set; }
        /// <summary>
        /// trường xóa => xóa mềm
        /// </summary>
        [JsonIgnore] 
        public bool IsDelete { get; set; }
        /// <summary>
        ///  
        /// liên kết 1- n với bảng laptop detail
        /// </summary>
        [JsonIgnore]
        public ICollection<LaptopDetail> laptopDetail { get; set; }
    }
}
