using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repos
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _db;

        public TodoRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<TodoItem> CreateAsync(TodoItem todo)
        {
            _db.TodoItems.Add(todo);
            await _db.SaveChangesAsync();
            return todo;
        }

        public async Task<IEnumerable<TodoItem>> GetAllByUserAsync(string userId)
        {
            return await _db.TodoItems.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id, string userId)
        {
            return await _db.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task UpdateAsync(TodoItem todo)
        {
            _db.TodoItems.Update(todo);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TodoItem todo)
        {
            _db.TodoItems.Remove(todo);
            await _db.SaveChangesAsync();
        }
    }
}
