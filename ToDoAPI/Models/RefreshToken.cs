using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; } = false;

        // FK to User
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
