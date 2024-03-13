using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Entity
{
    public class Storage
    {
        /// <summary>
        /// Id Kho hàng
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// số lượng bán ra
        /// </summary>
        [Range(0, 1000)]
        public int SoldNumber { get; set; }
        /// <summary>
        /// số lượng nhập
        /// </summary>
        [Required]
        [Range(0, 1000)]
        public int ImportNumber { get; set; }
        /// <summary>
        /// khóa ngoại liên kết với bảng Laptop Detail
        /// </summary>
        public int LaptopDetailId { get; set; }
        /// <summary>
        /// trường xóa
        /// </summary>
        [JsonIgnore] 
        public bool IsDelete { get; set; }
        /// <summary>
        /// liên kết với bảng laptop detail
        /// </summary>
        [JsonIgnore]
        public virtual LaptopDetail LaptopDetail { get; set; }
    }
}
