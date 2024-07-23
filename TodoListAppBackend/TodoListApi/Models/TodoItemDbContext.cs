using Microsoft.EntityFrameworkCore;

public class TodoItemDbContext : DbContext
{
    public TodoItemDbContext(DbContextOptions<TodoItemDbContext> options)
        : base(options) { }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}