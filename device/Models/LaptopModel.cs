using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class LaptopModel
    {
        /// <summary>
        /// Id Laptop
        /// </summary>

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
        public bool IsDelete { get; set; }
    }
}
