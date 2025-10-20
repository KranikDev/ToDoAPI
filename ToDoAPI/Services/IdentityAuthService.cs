using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class IdentityAuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;
        private readonly IEmailSender<ApplicationUser> _emailSender;

        public IdentityAuthService(UserManager<ApplicationUser> userManager, AppDbContext db, IEmailSender<ApplicationUser> emailSender)
        {
            _userManager = userManager;
            _db = db;
            _emailSender = emailSender;
        }

        public async Task<TokenResponseDto> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser { Email = dto.Email, UserName = dto.Email, FullName = dto.FullName };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailSender.SendConfirmationLinkAsync(user,user.Email, "Confirm your email");

            return await GenerateTokensAsync(user);
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) throw new ApplicationException("Invalid credentials");

            var valid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!valid) throw new ApplicationException("Invalid credentials");

            return await GenerateTokensAsync(user);
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var token = await _db.RefreshTokens.Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Token == refreshToken && !t.IsRevoked);

            if (token == null || token.Expires < DateTime.UtcNow)
                throw new ApplicationException("Invalid refresh token");

            token.IsRevoked = true;
            await _db.SaveChangesAsync();

            return await GenerateTokensAsync(token.User);
        }

        private async Task<TokenResponseDto> GenerateTokensAsync(ApplicationUser user)
        {
            var jwtToken = JwtService.GenerateToken(user);
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(7)
            };
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
}
