using Microsoft.AspNetCore.Mvc;
using warehouse_management_api.DTOs.Auth;
using warehouse_management_api.Services;
using Microsoft.AspNetCore.Authorization;

namespace warehouse_management_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _service.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _service.LoginAsync(dto);
            return Ok(token);
        }
    }
}