using device.IServices;
using device.Models;
using device.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace device.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        public UserController( ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var resultToken = await _service.Login(model);
            if (string.IsNullOrEmpty(resultToken)) 
            {
                return BadRequest("Email hoặc mật khẩu không đúng");
            }
            return Ok(new { token = resultToken });
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _service.Register(model);
            if (!result)
            {
                return BadRequest("Đăng ký không thành công");
            }
            return Ok();
        }
    }
}
