using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Entity
{
    public class Producer
    {
        /// <summary>
        /// Id nhà sản xuất
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// tên nhà sản xuất
        /// </summary>
        [Required, MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// trạng thái hoạt động của nhà sản xuất
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// trường xóa => xóa mềm
        /// </summary>
        [JsonIgnore] 
        public bool IsDelete { get; set; }
        /// <summary>
        /// liên kết 1- n với bảng laptop
        /// </summary>
        [JsonIgnore]
        public ICollection<Laptop> Laptops { get; set;}
    }
}
