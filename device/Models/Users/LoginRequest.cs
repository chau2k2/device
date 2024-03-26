using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Vui lòng nhập Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}
