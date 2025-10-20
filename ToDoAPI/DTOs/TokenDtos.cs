namespace TodoApi.DTOs
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }

    public class RefreshRequestDto
    {
        public string RefreshToken { get; set; } = null!;
    }
}
