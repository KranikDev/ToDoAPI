using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;

        // FK to User
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
