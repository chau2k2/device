using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace device.Entity
{
    public class User : IdentityUser<int>
    {
        public User() { }
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
