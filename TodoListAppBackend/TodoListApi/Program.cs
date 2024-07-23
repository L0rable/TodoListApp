using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoItemDbContext>(opt => opt.UseInMemoryDatabase("TodoItemList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoItemsAPI";
    config.Title = "TodoItemsAPI v1";
    config.Version = "v1";
});

// All Cross-Origin Resource Sharing
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowLocalhost");

// Populate the In-memory db
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    using (var context = new TodoItemDbContext(
        services.GetRequiredService<DbContextOptions<TodoItemDbContext>>()))
    {

        // Look for any TodoItems.
        if (context.TodoItems.Any())
        {
            return;
        }

        context.TodoItems.AddRange(
            new TodoItem { Name = "Task 1" },
            new TodoItem { Name = "Task 2" },
            new TodoItem { Name = "Task 3" }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
app.UseRouting();
app.MapControllers();

app.Run();
