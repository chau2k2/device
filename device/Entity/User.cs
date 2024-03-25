using System.ComponentModel.DataAnnotations;

namespace device.Entity
{
    public class User
    {
        /// <summary>
        /// khóa chính id (int)
        /// </summary>
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
        public string Password { get; set; }
    }
}
