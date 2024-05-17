using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using System.Diagnostics;

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

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(Guid id, TodoItem item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        // Retrieve the existing entity from the database
        var existingItem = await _context.TodoItems.FindAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        // Update the existing entity with the new values
        existingItem.Task = item.Task;
        existingItem.Deadline = item.Deadline;
        existingItem.Details = item.Details;
        existingItem.IsComplete = item.IsComplete;
        existingItem.SubTasks = item.SubTasks; // Assuming this handles the sub-tasks correctly

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(existingItem);
    }

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
        return _context.TodoItems.Any(e => e.Id == id);
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

        };
    }
}