using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("/todoItems")]
[ApiController]
public class TodoItemController : ControllerBase
{
    private readonly TodoItemDbContext dbContext;

    public TodoItemController(TodoItemDbContext todoItemDbContext)
    {
        dbContext = todoItemDbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {
        return await dbContext.TodoItems.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
    {
        dbContext.TodoItems.Add(todoItem);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodoItems), new { id = todoItem.Id }, todoItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await dbContext.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        dbContext.TodoItems.Remove(todoItem);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}