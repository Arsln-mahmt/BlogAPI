using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("Burası herkesin erişebileceği public endpoint.");
        }

        [Authorize]
        [HttpGet("private")]
        public IActionResult Private()
        {
            var userId = User.Identity?.Name ?? "Kimlik bilgisi yok";
            return Ok($"Burası sadece giriş yapmış kullanıcılar içindir. User: {userId}");
        }
    }
}
