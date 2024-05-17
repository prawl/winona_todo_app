namespace TodoApi.Models;

public class TodoItem
{
    public Guid? Id { get; set; }
    public string? Task { get; set; }
    public string? Deadline { get; set; }
    public string? Details { get; set; }
    public bool? IsComplete { get; set; }
    public string? Secret { get; set; }
}