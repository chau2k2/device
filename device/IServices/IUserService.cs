using device.Models;
using device.Response;
using device.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace device.IServices
{
    public interface IUserService
    {
        Task<bool> Login (LoginRequest request);
        Task<bool> Register (RegisterRequest request);
    }
}
