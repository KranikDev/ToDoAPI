namespace TodoApi.DTOs
{
    public class TodoCreateDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class TodoUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
    }

    public class TodoReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
