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
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
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

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var existingItem = await _context.TodoItems
                .Include(t => t.SubTasks)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingItem == null)
            {
                return NotFound();
            }

            // Detach existing item
            _context.Entry(existingItem).State = EntityState.Detached;

            // Update existing item's properties
            existingItem.Task = item.Task;
            existingItem.Deadline = item.Deadline;
            existingItem.Details = item.Details;
            existingItem.IsComplete = item.IsComplete;

            // Handle subtasks
            foreach (var subTask in item.SubTasks)
            {
                if (subTask.Id == Guid.Empty || subTask.Id == default)
                {
                    subTask.Id = Guid.NewGuid();
                }
                else
                {
                    var existingSubTask = await _context.TodoItems.FindAsync(subTask.Id);
                    if (existingSubTask != null)
                    {
                        _context.Entry(existingSubTask).State = EntityState.Detached;
                    }
                }
            }

            existingItem.SubTasks = item.SubTasks;

            // Attach the updated item to the context and update
            _context.Entry(existingItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(item);
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
    }
}
