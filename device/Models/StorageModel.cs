using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class StorageModel
    {
        /// <summary>
        /// Id Kho hàng
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// số lượng bán ra
        /// </summary>
        [Range(0, 1000, ErrorMessage = "nhap gia tri trong khoang 0 den 1000")]
        public int SoldNumber { get; set; }
        /// <summary>
        /// số lượng nhập
        /// </summary>
        [Required]
        [Range(0, 1000, ErrorMessage = "nhap gia tri trong khoang 0 den 1000")]
        public int ImportNumber { get; set; }
        /// <summary>
        /// trường xóa
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
