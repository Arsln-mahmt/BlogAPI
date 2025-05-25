using BlogAPI.DTOs;
using BlogAPI.Identity;
using BlogAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // ✅ Kayıt olma (Register)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _userService.RegisterAsync(dto);

            if (result.StartsWith("Invalid") || result.Contains("şifre"))
                return BadRequest(result);

            return Ok(new { Token = result });
        }

        // ✅ Giriş yapma (Login)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);

            if (result.StartsWith("Invalid"))
                return Unauthorized(result);

            return Ok(new { Token = result });
        }
    }
}
