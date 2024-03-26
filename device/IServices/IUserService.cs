using device.Models;
using device.System.Users;

namespace device.IServices
{
    public interface IUserService
    {
        Task<string> Login (LoginRequest request);
        Task<bool> Register (RegisterRequest request);
    }
}
