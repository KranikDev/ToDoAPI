using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto> RegisterAsync(RegisterDto dto);
        Task<TokenResponseDto> LoginAsync(LoginDto dto);
        Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);
    }
}
