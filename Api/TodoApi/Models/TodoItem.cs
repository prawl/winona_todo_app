using System.Collections.Generic;

namespace TodoApi.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public required string Task { get; set; }
    public required string Deadline { get; set; }
    public required string Details { get; set; }
    public bool IsComplete { get; set; }
    public IEnumerable<TodoItem>? SubTasks { get; set; }
}