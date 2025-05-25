using BlogAPI.DTOs;

namespace BlogAPI.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
