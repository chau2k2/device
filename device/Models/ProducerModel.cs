using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class ProducerModel
    {
        /// <summary>
        /// Id nhà sản xuất
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// tên nhà sản xuất
        /// </summary>
        [Required, MaxLength(50, ErrorMessage = "Length of name is not greater than 100")]
        public string Name { get; set; }
        /// <summary>
        /// trạng thái hoạt động của nhà sản xuất
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// trường xóa => xóa mềm
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
