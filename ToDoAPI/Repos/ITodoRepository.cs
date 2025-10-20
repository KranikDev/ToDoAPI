using TodoApi.Models;

namespace TodoApi.Repos
{
    public interface ITodoRepository
    {
        Task<TodoItem> CreateAsync(TodoItem todo);
        Task<IEnumerable<TodoItem>> GetAllByUserAsync(string userId);
        Task<TodoItem?> GetByIdAsync(int id, string userId);
        Task UpdateAsync(TodoItem todo);
        Task DeleteAsync(TodoItem todo);
    }
}
