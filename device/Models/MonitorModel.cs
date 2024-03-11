using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class MonitorModel
    {
        /// <summary>
        /// id Màn 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// tên màn
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "length of name must not be greater than 50")]
        public string Name { get; set; }
        /// <summary>
        /// giá của màn hình ( monitor)
        /// </summary>
        [Range(0, 100000000)]
        public decimal Price { get; set; }
        /// <summary>
        /// trường xóa => xóa mềm
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
