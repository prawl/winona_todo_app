using System.Collections.Generic;

namespace TodoApi.Models
{
    public class CandidateTodoItem
    {
        public required string Task { get; set; }
        public required string Deadline { get; set; }
        public required string Details { get; set; }
        public bool IsComplete { get; set; }
        public IEnumerable<CandidateTodoItem>? SubTasks { get; set; }
    }
}