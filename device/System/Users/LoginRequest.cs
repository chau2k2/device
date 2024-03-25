using System.ComponentModel.DataAnnotations;

namespace device.System.Users
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}
