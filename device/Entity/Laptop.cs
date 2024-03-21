using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Entity
{
    public class Laptop
    {
        /// <summary>
        /// Id Laptop
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// tên của Laptop độ dài tối đa 50
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// khóa ngoại liên kết với bảng Producer( nhà sản xuất)
        /// </summary>
        [Required]
        public int ProducerId { get; set; }
        /// <summary>
        /// giá nhập
        /// </summary>
        [Range(0, 10000000)]
        public decimal CostPrice { get; set; }
        /// <summary>
        /// giá bán
        /// </summary>
        [Required]
        [Range(0, 10000000)]
        public decimal SoldPrice { get; set; }
        /// <summary>
        /// dùng để xóa mềm
        /// </summary>
        [JsonIgnore] 
        public bool IsDelete { get; set; }
        /// <summary>
        /// liên kết với bảng producer( nhà sản xuất)
        /// </summary>
        [JsonIgnore]     
        public Producer? Producer { get; set; }
        /// <summary>
        /// liên kết 1 - n với bảng chi tiết Laptop (LaptopDetail)
        /// </summary>
        [JsonIgnore]
        public ICollection<LaptopDetail> LaptopDetail { get; set; }
        /// <summary>
        /// liên kết 1 - 1 vói bảng Kho hàng ( storage)
        /// </summary>

    }
}
