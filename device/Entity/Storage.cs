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
        /// số lượng hàng còn lại
        /// </summary>
        [Range(0, 500)]
        public int inventory { get; set; }
        /// <summary>
        /// loại sản phẩm: Laptop, PC
        /// </summary>
        public int ProductType { get; set; }
        /// <summary>
        /// tên sản phẩm
        /// </summary>
        public string? ProductName { get; set; } 
        /// <summary>
        /// trường xóa
        /// </summary>
        [JsonIgnore] 
        public bool IsDelete { get; set; }
    }
}
