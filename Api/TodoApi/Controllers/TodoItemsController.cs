using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
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
            return await _context.TodoItems.Include(t => t.SubTasks).ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(Guid id)
        {
            var todoItem = await _context.TodoItems
                .Include(t => t.SubTasks)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(CandidateTodoItem item)
        {
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = item.Task,
                Deadline = item.Deadline,
                Details = item.Details,
                IsComplete = item.IsComplete
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
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

        [HttpPut]
        public async Task<IActionResult> PutTodoItem(TodoItem todoItem)
        {

            var existingTodoItem = await _context.TodoItems
                .FirstOrDefaultAsync(t => t.Id == todoItem.Id);

            if (existingTodoItem == null)
            {
                return NotFound();
            }

            // Update existing TodoItem properties
            existingTodoItem.Task = todoItem.Task;
            existingTodoItem.Deadline = todoItem.Deadline;
            existingTodoItem.Details = todoItem.Details;
            existingTodoItem.IsComplete = todoItem.IsComplete;

            // Handle subtasks
            foreach (var subTask in todoItem.SubTasks)
            {
                if (subTask.Id == Guid.Empty)
                {
                    // New subtask, add to Subtasks DbSet
                    subTask.Id = Guid.NewGuid(); // Assign a new ID
                    subTask.ParentId = existingTodoItem.Id; // Set the ParentId
                    _context.Subtasks.Add(subTask);
                    existingTodoItem.SubTasks.Add(subTask);
                }
                else
                {
                    // Existing subtask, update it
                    var existingSubTask = await _context.Subtasks
                        .FirstOrDefaultAsync(s => s.Id == subTask.Id);

                    if (existingSubTask != null)
                    {
                        existingSubTask.Task = subTask.Task;
                        existingSubTask.Deadline = subTask.Deadline;
                        existingSubTask.Details = subTask.Details;
                        existingSubTask.IsComplete = subTask.IsComplete;
                        existingSubTask.ParentId = existingTodoItem.Id; // Ensure the ParentId is correct
                        _context.Subtasks.Update(existingSubTask);
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(todoItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(existingTodoItem);
        }

        private bool TodoItemExists(Guid id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

    }
}
