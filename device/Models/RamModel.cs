using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class RamModel
    {
        /// <summary>
        /// id Ram
        /// </summary>
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
        public bool IsDelete { get; set; }
    }
}
