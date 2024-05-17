using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoContext _context;

    public TodoItemsController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {
        return await _context.TodoItems
            .ToListAsync();
    }

    // GET: api/TodoItems/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodoItem(Guid id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return todoItem;
    }
    // </snippet_GetByID>

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut]
    public async Task<IActionResult> PutTodoItem([FromBody] TodoItem item)
    {
        var todoItem = await _context.TodoItems.FindAsync(item.Id);
        if (todoItem == null)
        {
            return NotFound();
        }

        todoItem.Task = item.Task;
        todoItem.Deadline = item.Deadline;
        todoItem.Details = item.Details;
        todoItem.IsComplete = item.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TodoItemExists(item.Id))
        {
            return NotFound();
        }

        return NoContent();
    }
    // </snippet_Update>

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult<TodoItem>> PostTodoItem([FromBody] CandidateTodoItem candidate)
    {
        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            IsComplete = candidate.IsComplete,
            Task = candidate.Task,
            Deadline = candidate.Deadline,
            Details = candidate.Details
        };

        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTodoItem),
            new { id = todoItem.Id },
            todoItem);
    }
    // </snippet_Create>

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(Guid id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoItemExists(Guid id)
    {
        return _context.TodoItems.Any(e => e.Id.ToString().ToLower() == id.ToString().ToLower());
    }

    private static TodoItem ToTodoItem(CandidateTodoItem todoItem)
    {
        return new TodoItem
        {
            Id = Guid.NewGuid(),
            Task = todoItem.Task,
            Deadline = todoItem.Deadline,
            Details = todoItem.Details,
            IsComplete = todoItem.IsComplete,
            SubTasks = todoItem.SubTasks as IEnumerable<TodoItem>
        };
    }
}