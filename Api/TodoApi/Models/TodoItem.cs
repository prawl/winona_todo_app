using System.Collections.Generic;

namespace TodoApi.Models;

public class TodoItem
{
<<<<<<< HEAD
    public Guid Id { get; set; }
    public required string Task { get; set; }
    public required string Deadline { get; set; }
    public required string Details { get; set; }
    public bool IsComplete { get; set; }
    public IEnumerable<TodoItem>? SubTasks { get; set; }
=======
    public Guid? Id { get; set; }
    public string? Task { get; set; }
    public string? Deadline { get; set; }
    public string? Details { get; set; }
    public bool? IsComplete { get; set; }
    public string? Secret { get; set; }
    // public List<TodoApi.Models.TodoItem>? SubTasks { get; set; }


>>>>>>> 6314c53 (Add Tests)
}