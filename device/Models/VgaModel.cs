using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class VgaModel
    {
        /// <summary>
        /// Id Card đồ họa
        /// </summary>
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
        public bool IsDelete { get; set; }
    }
}
